using AcademiaDanca.IO.Dominio.Contexto.Query.Financeiro.Mensalidade;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Infra.Repositorio
{
    public class RelatorioRepositorio : IRelatorioRepositorio
    {
        private readonly AcademiaContexto _contexto;

        public RelatorioRepositorio(AcademiaContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<EsperadoRealizadoQueryResultado>> ObterMensalidadesEsperadoRealizadoAsync(int? mes, int ano)
        {
            try
            {
                var parametros = new DynamicParameters();
                
                parametros.Add("sp_mes", mes);
                parametros.Add("sp_ano", ano);
                var query = @"SELECT 
                             E.Ano as Ano,
    E.Mes as Mes,
    Sum(E.TotalAluno) as AlunoTotalEsperado,
	Sum(R.TotalAluno) as AlunoTotalRealizado,
    Sum(E.totalMensalidade) as MensalidadeTotalEsperado,
	Sum(R.totalMensalidade) as MensalidadeTotalRealizado,
   CONVERT(Sum(E.ValorTotal), DECIMAL(10,2)) as ValorTotalEsperado,
   CONVERT(sum(R.valorTotal), DECIMAL(10,2))  as ValorTotalRealizado
                             FROM academia.view_valor_mensalidade_esperado AS E
                             Left Join academia.view_valor_mensalidade_realizado AS R ON E.Ano = R.ano and E.mes = R.mes 
                             Where E.Ano = @sp_ano
                             group by  E.Ano,E.Mes, R.Ano,R.Mes
order by E.Mes;";
                var esperadoRealizado = await _contexto.Connection.QueryAsync<EsperadoRealizadoQueryResultado>(
                    query, param: parametros, commandType: CommandType.Text
                    );
                return esperadoRealizado;

            }
            finally
            {
                _contexto.Dispose();
            }
        }

        public async Task<IEnumerable<MensalidadeVencidaQueryResultado>> ObterMensalidadesVencidasAsync(int? idMatricula, int? idMes, int? idAluno)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id_matricula", idMatricula);
                parametros.Add("sp_id_mes", idMes);
                parametros.Add("sp_id_aluno", idAluno);
                var query = @"sp_select_devedores";
                var mensalidades = await _contexto.Connection.QueryAsync<MensalidadeVencidaQueryResultado>(
                    query, param: parametros, commandType: CommandType.StoredProcedure
                    );
                return mensalidades;

            }
            finally
            {
                _contexto.Dispose();
            }
        }
    }
}
