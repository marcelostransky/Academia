using AcademiaDanca.IO.Dominio.Contexto.Query;
using AcademiaDanca.IO.Dominio.Contexto.Query.Aluno;
using AcademiaDanca.IO.Dominio.Contexto.Query.Financeiro;
using AcademiaDanca.IO.Dominio.Contexto.Query.Financeiro.Matricula;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Dapper;
using Slapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Infra.Repositorio
{
    public class MatriculaRepositorio : IMatriculaRepositorio
    {
        private readonly AcademiaContexto _contexto;

        public MatriculaRepositorio(AcademiaContexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<MatriculaQueryResultado> ObterMatriculaCompletoAsync(Guid id)
        {
            try
            {
                var query = @"SELECT Distinct
                          a.id as MatriculaAluno_Id,
                          a.nome as MatriculaAluno_Nome,
                          a.uif_id as MatriculaAluno_AlunoGuid,
                          ma.id as MatriculaBase_MatriculaId,
                          ma.valor_contrato as MatriculaBase_MatriculaValorContrato,
                          ma.percentual_desconto as MatriculaBase_MatriculaPercentualDesconto,
                          ma.valor_desconto as MatriculaBase_MatriculaValorDesconto,
                          ma.dia_venciamento as MatriculaBase_MatriculaDiaVencimento,
                          ma.data_inicial_pagamento as MatriculaBase_MatriculaDataInicialPagamento,
                          ma.uif_id as MatriculaBase_MatriculaGuid,
                          ma.valor_matricula as MatriculaBase_MatriculaValorMatricula,
                          ma.total_parcelas as MatriculaBase_MatriculaTotalParcelas,
                          ma.ano as MatriculaBase_MatriculaAno,
                          ma.mes_inicio_pagamento as  MatriculaBase_MatriculaMesInicioPagamento,
                          case when ma.status = 1 then 'Ativo' else 'Inativo' end as MatriculaBase_MatriculaStatus,
                          mt.id_turma as MatriculaItens_IdTurma,
                          mt.valor as MatriculaItens_Valor,
                          case when mt.desconto = 1 then 1 else 0 end as MatriculaItens_Desconto,
                          mt.valor_desconto as MatriculaItens_ValorDesconto,
                          mt.valor_calculado as MatriculaItens_ValorCalculado,
                          t.cod_turma as MatriculaItens_CodTurma
                          FROM  academia.aluno as a
                          join academia.matricula as ma on a.id = ma.id_aluno
                          left Join academia.matricula_turma as mt on ma.id = mt.id_matricula
                          left join academia.turma as t on mt.id_turma = t.id
                          where  a.uif_id = @id
                          Order by a.id;";
                var parametros = new DynamicParameters();
                parametros.Add("id", id.ToString());
                MatriculaQueryResultado matricula = new MatriculaQueryResultado();
                var matriculaRetorno = (await _contexto.Connection.QueryAsync<dynamic>(query,
                parametros, commandType: CommandType.Text));
                AutoMapper.Configuration.AddIdentifier(typeof(AlunoQueryResultado), "AlunoId");
                AutoMapper.Configuration.AddIdentifier(typeof(AlunoTurmaQuery), "TurmaId");
                AutoMapper.Configuration.AddIdentifier(typeof(MatriculaQueryResultado), "MatriculaId");
                AutoMapper.Configuration.AddIdentifier(typeof(ItemMatriculaQueryResultado), "IdTurma");
                matricula = (AutoMapper.MapDynamic<MatriculaQueryResultado>(matriculaRetorno)).FirstOrDefault();
                CarregarItensTemp(matricula.MatriculaBase.MatriculaGuid);

                return matricula;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _contexto.Dispose();
            }
        }
        public void CarregarItensTemp(string id)
        {
            var query = @" 
                            Delete From academia.matricula_turma_temp  where id_matricula = @id;
                            INSERT INTO `academia`.`matricula_turma_temp`
                          (`id_matricula`,
                           `id_turma`,
                           `valor`,
                           `desconto`,
                           `valor_desconto`,
                           `valor_calculado`)
                            SELECT m.uif_id,ma.id_turma,ma.valor,ma.desconto,ma.valor_desconto,ma.valor_calculado 
                            FROM academia.matricula as m
                            Join academia.matricula_turma as ma on m.id = id_matricula where M.uif_id = @id;";
            var parametros = new DynamicParameters();
            parametros.Add("id", id);
            _contexto.Connection.ExecuteAsync(query, param: parametros, commandType: CommandType.Text);
        }

        public async Task<MatriculaSimplificadoQueryResultado> ObterPor(Guid id)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id", id);
                var query = @"SELECT 
                        id as IdMatricula, 
                        id_aluno as IdAluno,
                        case when status = 1 then true else false end as status
                        FROM academia.matricula where uif_id = @sp_id;";
                var matricula = await _contexto.Connection.QueryAsync<MatriculaSimplificadoQueryResultado>(
                    query, param: parametros, commandType: CommandType.Text
                    );
                return matricula.FirstOrDefault();

            }
            finally
            {
                _contexto.Dispose();
            }

        }
        public async Task<int> DeletarItemMatricula(int idMatricula, int idTurma = 0)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id_turma", idTurma);
                parametros.Add("sp_id_matricula", idMatricula);
                var query = $"Delete From academia.matricula_turma Where id_matricula = @sp_id_matricula ";
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
        public async Task<int> InativarAsync(int idMatricula)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id_matricula", idMatricula);
                var query = $"Update  academia.matricula set status = 0 Where id = @sp_id_matricula ";

                var inativado = (await _contexto
                      .Connection
                      .ExecuteAsync(query,
                      parametros,
                      commandType: System.Data.CommandType.Text));

                return inativado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Sistema não conseguiu inativar a matricula.<br>Erro: [{ex.Message}]");
            }
            finally
            {
                _contexto.Dispose();
            }
        }

    }
}
