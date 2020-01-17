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
                          join academia.turma as t on mt.id_turma = t.id
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
    }
}
