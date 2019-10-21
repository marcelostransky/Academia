using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AcademiaDanca.IO.App.Filtros;
using AcademiaDanca.IO.App.Helper;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.AcessoComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Acesso;
using AcademiaDanca.IO.Dominio.Contexto.Query.Acesso;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Leanwork.CodePack.DataTables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace AcademiaDanca.IO.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [PermissaoAcesso(PaginaId = "PERM", Verbo = "Ler", TipoRetorno = "Html")]
    public class PermissaoController : Controller
    {
        private readonly IAcessoRepositorio _repositorio;
        public readonly AddPermissaoManipulador _manipuladorPermissao;
        public readonly DelPermissaoManipulador _manipuladorDelPermissao;
        public readonly EditarPermissaoManipulador _manipuladorEditarPermissao;
        private readonly IConfiguration _config;
        public PermissaoController(IAcessoRepositorio repositorio
       , AddPermissaoManipulador manipuladorPermissao
            , DelPermissaoManipulador manipuladorDelPermissao
            , EditarPermissaoManipulador manipuladorEditarPermissao
            , IConfiguration config)
        {
            _repositorio = repositorio;

            _manipuladorPermissao = manipuladorPermissao;
            _manipuladorDelPermissao = manipuladorDelPermissao;
            _manipuladorEditarPermissao = manipuladorEditarPermissao;
            _config = config;
        }
        public async Task<IActionResult> Index()
        {
            var paginas = new SelectList(await _repositorio.ObterPaginasAsync(), "Constante", "DesPagina");
            var perfis = new SelectList(await _repositorio.ObterPerfisAsync(), "Id", "DesPerfil");
            ViewBag.Paginas = paginas;
            ViewBag.Perfis = perfis;
            return await Task.Run(() => View());
        }
        [Route("/Admin/Permissao/Excluir/")]
        [HttpDelete]
        [PermissaoAcesso(PaginaId = "PERM", Verbo = "Excluir", TipoRetorno = "Json")]
        public async Task<IActionResult> Excluir(DelPermissaoComando comando)
        {
            try
            {
                var resultado = await _manipuladorDelPermissao.ManipuladorAsync(comando);

                return Json(resultado);
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }
        }
        [HttpPost]
        [Route("/Admin/Permissao/Criar")]
        [PermissaoAcesso(PaginaId = "PERM", Verbo = "Criar", TipoRetorno = "Json")]
        public async Task<IActionResult> Criar(AddPermissaoComando comando)
        {
            try
            {
                var resultado = await _manipuladorPermissao.ManipuladorAsync(comando);
                var perfil = comando.DesPerfil.Trim();
                new Util(_config).RemoverMenuCache(perfil);
                new Util(_config).RemoverPermissaoCache(perfil);

                return Json(resultado);
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }
        }
        [HttpPut]
        [Route("/Admin/Permissao/Editar")]
        [PermissaoAcesso(PaginaId = "PERM", Verbo = "Editar", TipoRetorno = "Json")]
        public async Task<IActionResult> Editar(EditarPermissaoComando comando)
        {
            try
            {
                var resultado = await _manipuladorEditarPermissao.ManipuladorAsync(comando);
                var perfil = comando.Constante.Trim();
                new Util(_config).RemoverMenuCache(perfil);
                new Util(_config).RemoverPermissaoCache(perfil);
                return Json(resultado);
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }
        }

        public async Task<IActionResult> Listar(string paginaId, string perfilId, jQueryDataTableRequestModel request)
        {
            try
            {

                var lista = (await _repositorio.ObterPermissaosAsync(paginaId.Equals("0") ? null : paginaId,
                                                                     perfilId.Equals("0") ? null : perfilId)).AsQueryable();

                if (request.sSearch != null && request.sSearch.Length > 0)
                {
                    lista = lista.Where(x => x.DesPagina.ToUpper().Contains(request.sSearch.ToUpper()) || x.DesPapel.ToUpper().Contains(request.sSearch.ToUpper()));
                }

                var model = (from r in lista
                             select new
                             {
                                 PermissaoId = $"{r.PapelId }{r.PaginaId}",
                                 r.PaginaId,
                                 r.PapelId,
                                 desPapel = $" {r.DesPapel }<input type = \"hidden\" value = \"{r.PapelId }\" />",
                                 desPagina = $" {r.DesPagina }<input type = \"hidden\" value = \"{r.PaginaId }\" />",
                                 Criar = $"<label class=\"custom-control custom-checkbox\"><input {(r.Criar ? "checked" : "")} type = \"checkbox\" class=\"custom-control-input\" /><span class=\"custom-control-indicator\"></span></label>",
                                 Editar = $"<label class=\"custom-control custom-checkbox\"><input {(r.Editar ? "checked" : "")} type = \"checkbox\" class=\"custom-control-input\" /><span class=\"custom-control-indicator\"></span></label>",
                                 Deletar = $"<label class=\"custom-control custom-checkbox\"><input {(r.Excluir ? "checked" : "")} type = \"checkbox\" class=\"custom-control-input\" /><span class=\"custom-control-indicator\"></span></label>",
                                 Ler = $"<label class=\"custom-control custom-checkbox\"><input {(r.Ler ? "checked" : "")} type = \"checkbox\" class=\"custom-control-input\" /><span class=\"custom-control-indicator\"></span></label>",
                                 Acao = ObterMenuAcaoDataTable(r)


                             }).DataTableResponse(request);
                return Ok(model);

            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        private object ObterMenuAcaoDataTable(PermissaoResultadoQuery r)
        {
            var perfil = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Papel").Value;
            StringBuilder menu = new StringBuilder();

            menu.AppendFormat("<a href =\"#\" onclick=Editar(this)  class=\"btn btn-icon fuse-ripple-ready\" title=\"Editar Permissão\"> <i class=\"icon-pencil-lock \"></i>    </a><a href =\"#\" onclick=Excluir(this)  class=\"btn btn-icon fuse-ripple-ready\" title=\"Excluir Permissão\"> <i class=\"icon-delete-forever  \"></i>    </a>", r.PapelId, r.PaginaId);

            return menu.ToString();
        }
    }
}