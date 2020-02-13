using AcademiaDanca.IO.Dominio.Contexto.Comandos.ConfiguracaoComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Repositorio
{
    public interface IParametroRepositorio
    {
        Task<IEnumerable<ParametroQueryResultado>> ObterParametrosAsync();
        Task Editar(ParametroItem parametro);

    }
}
