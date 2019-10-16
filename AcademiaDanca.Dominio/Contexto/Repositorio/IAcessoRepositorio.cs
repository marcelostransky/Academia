using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query.Acesso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Repositorio
{
    public interface IAcessoRepositorio
    {
        Task<int> SalvarAsync(Pagina pagina);
        Task<int> EditarAsync(Pagina pagina);
        Task<int> EditarPerfilAsync(Perfil perfil);
        Task<int> DeletarAsync(int id);
        Task<bool> DeletarPermissaoAsync(int perfilId, int paginaId);
        Task<IEnumerable<PaginaResultadoQuery>> ObterPaginasAsync();
        Task<IEnumerable<PermissaoResultadoQuery>> ObterPermissaosAsync(string paginaId, string perfilId);
        Task<IEnumerable<PerfilResultadoQuery>> ObterPerfisAsync();
        Task<bool> CheckPaginaAsync(string desPagina, string chave);
   
        Task<bool> CheckPerfilAsync(string desPerfil);
        List<PermissaoResultadoQuery> ObterPermissaosAsync(string perfil);
        Task<int> SalvarPerfilAsync(Perfil perfil);
        Task<bool> CheckPermissaoAsync(int? paginaId, int? perfilId);
        Task<bool> DeletarPaginaAsync(int id);
        Task<bool> CheckFuncionarioPerfilAsync(int id);
        Task<bool> DeletarPerfilAsync(int id);
        Task<int> SalvarPermissaoAsync(Permissao permissao);
        Task<int> EditaPaginaAsync(Pagina pagina);
    }
}
