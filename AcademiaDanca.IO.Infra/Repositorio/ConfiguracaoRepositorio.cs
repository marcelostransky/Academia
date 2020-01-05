using AcademiaDanca.IO.Dominio.Contexto.Query.Configuracao;
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
    public class ConfiguracaoRepositorio : IConfiguracaoRepositorio
    {
        private readonly AcademiaContexto _contexto;

        public ConfiguracaoRepositorio(AcademiaContexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<ConfiguracaoQueryResultado> ObterPorChaveAsync(string chave)
        {
            try
            {
                var lista = await _contexto
                .Connection
                .QueryAsync<ConfiguracaoQueryResultado>("SELECT id as Id, chave as Chave, valor as Valor FROM academia.parametro where chave = @chave", new { chave = chave }, commandType: CommandType.Text);

                return lista.FirstOrDefault();
            }
            finally
            {
                _contexto.Dispose();
            }

        }
    }
}
