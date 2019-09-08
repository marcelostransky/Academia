using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query.Acesso;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Repositorio
{
    public interface IAcessoRepositorio
    {
        Task<int> SalvarAsync(Pagina pagina);
        Task<int> EditarAsync(Pagina pagina);
        Task<int> DeletarAsync(int id);
        Task<IEnumerable<PaginaResultadoQuery>> ObterPaginasAsync();
        Task<IEnumerable<PerfilResultadoQuery>> ObterPerfisAsync();
        Task<bool> CheckPaginaAsync(string desPagina);
        Task<bool> CheckPerfilAsync(string desPerfil);
        Task<int> SalvarPerfilAsync(Perfil perfil);
    }
}
