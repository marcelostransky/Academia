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
    public class TipoTelefoneRepositorio : ITipoTelefoneRepositorio
    {
        private readonly AcademiaContexto _contexto;

        public TipoTelefoneRepositorio(AcademiaContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> CheckNomeTipoTelefone(string desTipoTelefone)
        {
            var lista = await _contexto
                .Connection
                .QueryAsync<int>("SELECT Count(1) FROM academia.tipo_telefone where  des_tipo_telefone = @nome;",new {nome = desTipoTelefone}, commandType: CommandType.Text);

            return lista.FirstOrDefault() > 0 ? true : false;
        }

        public async Task<TipoTelefoneQueryResult> ObterPorAsync(int id)
        {
            var lista = await _contexto
               .Connection
               .QueryAsync<TipoTelefoneQueryResult>("sp_sel_tipo_telefone",new { sp_id = id }, commandType: CommandType.StoredProcedure);
            return lista.FirstOrDefault();
        }

        public async Task<IEnumerable<TipoTelefoneQueryResult>> ObterTodosAsync()
        {
            var lista = await _contexto
                .Connection
                .QueryAsync<TipoTelefoneQueryResult>("SELECT id as Id, des_tipo_telefone as DesTipoTelefone FROM academia.tipo_telefone;", commandType: CommandType.Text);
            return lista;
        }

        public async Task<int> SalvarAsync(TipoTelefone tipoTelefone)
        {
            var parametros = new DynamicParameters();

            parametros.Add("sp_id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("sp_des_tipo_telefone", tipoTelefone.DesTipoTelefone);
            await _contexto
                .Connection
                .ExecuteAsync("sp_insert_tipo_telefone",
                parametros,
                commandType: System.Data.CommandType.StoredProcedure);

            return parametros.Get<int>("sp_id");
        }
    }
}
