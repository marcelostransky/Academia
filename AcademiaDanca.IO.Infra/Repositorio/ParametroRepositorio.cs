using AcademiaDanca.IO.Dominio.Contexto.Comandos.ConfiguracaoComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Infra.Repositorio
{
    public class ParametroRepositorio : IParametroRepositorio
    {
        private readonly AcademiaContexto _contexto;

        public ParametroRepositorio(AcademiaContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task Editar(ParametroItem parametro)
        {
            try
            {
                var parametroDynamic = new DynamicParameters();
                parametroDynamic.Add("sp_chave", parametro.Chave);
                parametroDynamic.Add("sp_valor", parametro.Valor);
                await _contexto
                     .Connection
                     .ExecuteAsync(@"UPDATE academia.parametro
                                     SET  valor =  @sp_valor
                                     WHERE chave = @sp_chave; ",
                                     parametroDynamic,
                         commandType: System.Data.CommandType.Text);
            }
            finally
            {
                _contexto.Dispose();
            }
        }

        public async Task<IEnumerable<ParametroQueryResultado>> ObterParametrosAsync()
        {
            try
            {
                var parametros = await _contexto
                     .Connection
                     .QueryAsync<ParametroQueryResultado>("SELECT * FROM academia.parametro;",
                         commandType: System.Data.CommandType.Text);

                return parametros;
            }
            finally
            {
                _contexto.Dispose();
            }
        }
    }
}
