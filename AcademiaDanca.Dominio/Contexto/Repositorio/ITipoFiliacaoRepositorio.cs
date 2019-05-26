using AcademiaDanca.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Repositorio
{
    public interface ITipoFiliacaoRepositorio
    {
        Task<int> SalvarAsync(TipoFiliacao tipoTelefone);
        Task<IEnumerable<TipoFiliacaoQueryResultado>> ObterTodosAsync();
        Task<TipoFiliacaoQueryResultado> ObterPorAsync(int id);
        Task<bool> CheckNomeAsync(string desTipoTelefone);
    }
}
