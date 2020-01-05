using AcademiaDanca.IO.Dominio.Contexto.Query.Configuracao;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Academia.Io.Tests.Fakes
{
    public class FakeConfiguracaoRepositorio : IConfiguracaoRepositorio
    {
        public Task<ConfiguracaoQueryResultado> ObterPorChaveAsync(string chave)
        {
            throw new NotImplementedException();
        }
    }
}
