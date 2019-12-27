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
                  .QueryAsync<int>("SELECT count(1) FROM academia.matricula where id_aluno = @sp_id_aluno and ano = @sp_ano",
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

        public async Task<int> RegistrarItemMatricula(int idTurma, int idMatricula, decimal valor)
        {
            var parametros = new DynamicParameters();
            parametros.Add("sp_id_turma", idTurma);
            parametros.Add("sp_id_matricula", idMatricula);
            parametros.Add("sp_valor_turma", valor);
            var registrado = (await _contexto
                  .Connection
                  .ExecuteAsync("sp_insert_item_matricula",
                  parametros,
                  commandType: System.Data.CommandType.StoredProcedure));

            return registrado;
        }
    }
}
