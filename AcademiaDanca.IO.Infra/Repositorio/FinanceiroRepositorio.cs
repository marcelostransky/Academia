using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada.Com_Matricula;
using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query.Financeiro;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Infra.Repositorio
{
    public class FinanceiroRepositorio : IFinanceiroRepositorio
    {
        private readonly AcademiaContexto _contexto;

        public FinanceiroRepositorio(AcademiaContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> CheckMatriculaExisteAsync(Matricula matricula)
        {
            var parametros = new DynamicParameters();
            parametros.Add("sp_id_aluno", matricula.IdAluno);
            parametros.Add("sp_ano", matricula.Ano);
            var existe = (await _contexto
                  .Connection
                  .QueryAsync<int>("SELECT count(1) FROM academia.matricula where id_aluno = @sp_id_aluno and ano = @sp_ano and status = 1",
                  parametros,
                  commandType: System.Data.CommandType.Text)).FirstOrDefault();
            _contexto.Dispose();
            return existe > 0;
        }
        public async Task<int> MatricularAsync(Matricula matricula)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("sp_id_aluno", matricula.IdAluno);
                parametros.Add("sp_valor_contrato", matricula.ValorContrato);
                parametros.Add("sp_percentual_desconto", matricula.PercentualDesconto);
                parametros.Add("sp_valor_desconto", matricula.ValorDesconto);
                parametros.Add("sp_dia_vencimento", matricula.DiaVencimento);
                parametros.Add("sp_data_inicial_pagamento", matricula.DataIncialPagamento);
                parametros.Add("sp_uif_id", matricula.ChaveRegistro);
                parametros.Add("sp_valor_matricula", matricula.ValorMaricula);
                parametros.Add("sp_data_contrato", matricula.DataContrato);
                parametros.Add("sp_data_criacao", matricula.DataCriacao);
                parametros.Add("sp_total_parcelas", matricula.TotalParcelas);
                parametros.Add("sp_ano", matricula.Ano);
                parametros.Add("sp_mes_inicio_pagamento", matricula.MesInicioPagamento);
                await _contexto
                    .Connection
                    .ExecuteAsync("sp_insert_matricula",
                    parametros,
                    commandType: System.Data.CommandType.StoredProcedure);

                return parametros.Get<int>("sp_id");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task GerarMensalidade(Mensalidade mensalidade)
        {
            var lista = mensalidade.Mensalidades();
            foreach (var mes in lista)
            {
                try
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("sp_id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    parametros.Add("sp_id_matricula", mes.IdMatricula);
                    parametros.Add("sp_data_vencimento", mes.DataVencimento);
                    parametros.Add("sp_valor", mes.Valor);
                    parametros.Add("sp_desconto", mes.Desconto);
                    parametros.Add("sp_parcela", mes.Parcela);
                    parametros.Add("sp_id_aluno", mes.IdAluno);

                    await _contexto
                        .Connection
                        .ExecuteAsync("sp_insert_mensalidade",
                        parametros,
                        commandType: System.Data.CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<List<MensalidadesQueryResultado>> ObterMensalidadesPorAlunoAsync(Guid? uifIdAluno, string status, int? ano)
        {

            var limit = string.Empty;
            var data = string.Empty;
            var dataConsulta = ObterDataFormatoUnix(DateTime.Now);
            var parametros = new DynamicParameters();
            parametros.Add("sp_id_aluno", uifIdAluno);
            parametros.Add("sp_data_consulta", dataConsulta);

            if (uifIdAluno.Equals(null))
            {
                limit = "limit 100";
            }
            switch (status)
            {
                case "Vencida":
                    data = $" and M.Pago = 0 and  M.data_vencimento < @sp_data_consulta";
                    break;
                case "Avencer":
                    data = $" and M.Pago = 0  and  M.data_vencimento > @sp_data_consulta";
                    break;
                case "Hoje":
                    data = $" and  M.data_vencimento = @sp_data_consulta";
                    break;
                case "Pago":
                    data = $" and  M.Pago = 1";
                    break;

            }
            if (!ano.Equals(null))
            {
                parametros.Add("sp_Ano", ano);
                data += $" and  year(M.data_vencimento) = @sp_Ano";
            }
            var query = $@"SELECT  M.id as  MensalidadeId, 
                                                            M.id_aluno as AlunoId,
                                                            a.uif_id AS GuidAluno,
                                                            M.parcela as Parcela,
                                                            M.valor as Valor, M.data_vencimento as DataVencimento,
                                                            M.desconto as Desconto,
                                                            M.pago as Pago,
                                                            M.data_pagamento as DataPagamento,
                                                            M.juros as Juros,
                                                            A.nome as AlunoNome FROM academia.mensalidade as M
                                                            join academia.aluno as A on M.id_aluno = A.id
                                                            where  a.uif_id = ifnull(@sp_id_aluno,a.uif_id) {data}  {limit}";
            var resultado = (await _contexto
                  .Connection
                  .QueryAsync<MensalidadesQueryResultado>(query,
                  parametros,
                  commandType: System.Data.CommandType.Text)).ToList();

            return resultado;
        }

        private string ObterDataFormatoUnix(DateTime data)
        {
            var d = data.ToShortDateString().Split('/');
            return $"{d[2]}-{d[1]}-{d[0]}";
        }

        public async Task<bool> RegistrarPagamentoAsync(int idMensalidade, bool pago, double juros)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id", idMensalidade);
                parametros.Add("sp_pago", pago);
                parametros.Add("sp_juros", juros);
                parametros.Add("sp_data", DateTime.Now);

                var query = "update academia.mensalidade set pago = @sp_pago , juros = @sp_juros, data_pagamento = @sp_data where id = @sp_id";
                var atualizado = await _contexto
                     .Connection
                     .ExecuteAsync(query,
                     parametros,
                     commandType: System.Data.CommandType.Text);
                return atualizado > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> RegistrarItemMatricula(MatriculaItem item)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id_turma", item.IdTurma);
                parametros.Add("sp_id_matricula", item.IdMatricula);
                parametros.Add("sp_valor_turma", item.Valor);
                parametros.Add("sp_desconto", item.Desconto ? 1 : 0);
                parametros.Add("sp_valor_desconto", item.ValorDesconto);
                parametros.Add("sp_valor_calculado", item.ValorCalculado);
                var registrado = (await _contexto
                      .Connection
                      .ExecuteAsync("sp_insert_item_matricula",
                      parametros,
                      commandType: System.Data.CommandType.StoredProcedure));

                return registrado;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                _contexto.Dispose();
            }

        }




        public async Task<IEnumerable<ItemMatriculaQueryResultado>> ObterItensMatriculaPor(int id)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id_matricula", id);

                var listaRetorno = (await _contexto
                      .Connection
                      .QueryAsync<ItemMatriculaQueryResultado>(@"SELECT 
                      MT.id_matricula as IdMatricula,
                      MT.id_turma as IdTurma,
                      T.cod_turma as CodTurma,
                      MT.valor as Valor,
                      MT.desconto as Desconto,
                      MT.valor_desconto as ValorDesconto,
                      MT.valor_calculado as ValorValculado FROM academia.matricula_turma as MT
                      Join academia.turma as T ON MT.id_turma = T.id
                      Where MT.id_matricula = @sp_id_matricula",
                      parametros,
                      commandType: System.Data.CommandType.Text));

                return listaRetorno;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _contexto.Dispose();
            }
        }

        public async Task<bool> CheckMatriculaItemTempExisteAsync(MatriculaItemComando comando)
        {
            var query = @"SELECT count(1) FROM academia.matricula_turma_temp 
                          where id_matricula = @sp_id_matricula and id_turma = @sp_id_turma ;";
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id_matricula", comando.IdMatriculaGuid);
                parametros.Add("sp_id_turma", comando.IdTurma);

                var liberado = (await _contexto
                      .Connection
                      .QueryAsync<int>(query,
                      parametros,
                      commandType: System.Data.CommandType.Text)).FirstOrDefault() > 0 ? true : false;

                return liberado;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                _contexto.Dispose();
            }
        }
        public async Task<int> RegistrarItemMatriculaTemp(MatriculaItemTemp item)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id_turma", item.IdTurma);
                parametros.Add("sp_id_matricula", item.IdMatricula);
                parametros.Add("sp_valor_turma", item.Valor);
                parametros.Add("sp_desconto", item.Desconto ? 1 : 0);
                parametros.Add("sp_valor_desconto", item.ValorDesconto);
                parametros.Add("sp_valor_calculado", item.ValorCalculado);
                var registrado = (await _contexto
                      .Connection
                      .ExecuteAsync("sp_insert_item_matricula_temp",
                      parametros,
                      commandType: System.Data.CommandType.StoredProcedure));

                return registrado;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                _contexto.Dispose();
            }

        }
        public async Task<IEnumerable<ItemMatriculaQueryResultado>> ObterMatriculaItensTempPor(Guid id)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id_matricula", id.ToString());

                var listaRetorno = (await _contexto
                      .Connection
                      .QueryAsync<ItemMatriculaQueryResultado>(@"SELECT 
                      MT.id_matricula as IdMatriculaGuid,
                      MT.id_turma as IdTurma,
                      T.cod_turma as CodTurma,
                      MT.valor as Valor,
                      case when MT.desconto = 0 then false else true end as Desconto,
                      MT.valor_desconto as ValorDesconto,
                      MT.valor_calculado as ValorCalculado FROM academia.matricula_turma_temp as MT
                      Join academia.turma as T ON MT.id_turma = T.id
                      Where MT.id_matricula = @sp_id_matricula",
                      parametros,
                      commandType: System.Data.CommandType.Text));

                return listaRetorno;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                _contexto.Dispose();
            }
        }

        public async Task<int> DeletarItemMatriculaTemp(string idMatriculaGuid, int idTurma = 0)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id_turma", idTurma);
                parametros.Add("sp_id_matricula", idMatriculaGuid);
                var query = $"Delete From academia.matricula_turma_temp Where id_matricula =@sp_id_matricula ";
                if (idTurma > 0)
                {
                    query += " and id_turma = @sp_id_turma";
                }
                var deletado = (await _contexto
                      .Connection
                      .ExecuteAsync(query,
                      parametros,
                      commandType: System.Data.CommandType.Text));

                return deletado;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _contexto.Dispose();
            }
        }

        public async Task AtualizaItemMatriculaTemp(MatriculaItemTemp temp)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id_turma", temp.IdTurma);
                parametros.Add("sp_id_matricula", temp.IdMatricula);
                parametros.Add("sp_valor", temp.Valor);
                parametros.Add("sp_desconto", temp.Desconto ? 1 : 0);
                parametros.Add("sp_valor_desconto", temp.ValorDesconto);
                parametros.Add("sp_valor_calculado", temp.ValorCalculado);

                var query = @"UPDATE `academia`.`matricula_turma_temp`
                                SET
                               
                                `valor` = @sp_valor,
                                `desconto` = @sp_desconto,
                                `valor_desconto` = @sp_valor_desconto,
                                `valor_calculado` = @sp_valor_calculado
                                WHERE `id_matricula` = @sp_id_matricula
                                AND `id_turma` =@sp_id_turma;";
                await _contexto
                     .Connection
                     .ExecuteAsync(query,
                     parametros,
                     commandType: System.Data.CommandType.Text);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _contexto.Dispose();
            }
        }
    }
}
