using AcademiaDanca.IO.Dominio.Contexto.Query.Configuracao;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Repositorio
{
    public interface IConfiguracaoRepositorio
    {
        Task<ConfiguracaoQueryResultado> ObterPorChaveAsync(string chave);
    }
}
