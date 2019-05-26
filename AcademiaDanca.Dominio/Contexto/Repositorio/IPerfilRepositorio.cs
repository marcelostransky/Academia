using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Repositorio
{
    public interface IPerfilRepositorio
    {
        Task<int> SalvarAsync(Perfil perfil);
        Task<IEnumerable<PerfilQueryResultado>> ObterTodosAsync();
        Task<PerfilQueryResultado> ObterPorAsync(int id);
        Task<bool> CheckNomeAsync(string descPerfil);
    }
}
