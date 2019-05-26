using AcademiaDanca.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Repositorio
{
    public interface ITipoTelefoneRepositorio
    {
        Task<int> SalvarAsync(TipoTelefone tipoTelefone);
        Task<IEnumerable<TipoTelefoneQueryResult>> ObterTodosAsync();
        Task<TipoTelefoneQueryResult> ObterPorAsync(int id);
        Task<bool> CheckNomeTipoTelefone(string desTipoTelefone);
    }
}
