using AcademiaDanca.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Infra.Repositorio
{
    public class TipoFiliacaoRepositorio : ITipoFiliacaoRepositorio
    {
        private readonly AcademiaContexto _contexto;

        public TipoFiliacaoRepositorio(AcademiaContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> CheckNomeAsync(string desTipoFiliacao)
        {
            var lista = await _contexto
              .Connection
              .QueryAsync<int>("SELECT Count(1) FROM academia.tipo_filiacao where  des_tipo_filiacao = @nome;", new { nome = desTipoFiliacao }, commandType: CommandType.Text);

            return lista.FirstOrDefault() > 0 ? true : false;
        }

        public async Task<TipoFiliacaoQueryResultado> ObterPorAsync(int id)
        {
            var lista = await _contexto
              .Connection
              .QueryAsync<TipoFiliacaoQueryResultado>("sp_sel_tipo_filiacao", new { sp_id = id }, commandType: CommandType.StoredProcedure);
            return lista.FirstOrDefault();
        }

        public async Task<IEnumerable<TipoFiliacaoQueryResultado>> ObterTodosAsync()
        {
            var lista = await _contexto
               .Connection
               .QueryAsync<TipoFiliacaoQueryResultado>("SELECT id as Id, des_tipo_filiacao as DesTipoFiliacao FROM academia.tipo_filiacao;", commandType: CommandType.Text);
            return lista;
        }

        public async Task<int> SalvarAsync(TipoFiliacao tipoFiliacao)
        {
            var parametros = new DynamicParameters();

            parametros.Add("sp_id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("sp_des_tipo_filiacao", tipoFiliacao.DesTipoFiliacao);
            await _contexto
                .Connection
                .ExecuteAsync("sp_insert_tipo_Filiacao",
                parametros,
                commandType: System.Data.CommandType.StoredProcedure);

            return parametros.Get<int>("sp_id");
        }
    }
}
