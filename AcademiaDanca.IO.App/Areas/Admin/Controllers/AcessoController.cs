using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AcademiaDanca.IO.App.Filtros;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.AcessoComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Acesso;
using AcademiaDanca.IO.Dominio.Contexto.Query.Acesso;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Leanwork.CodePack.DataTables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AcademiaDanca.IO.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    //[PermissaoAcesso(PaginaId = "Acesso", Verbo = "Ler")]
    public class AcessoController : Controller
    {
        private readonly IAcessoRepositorio _repositorio;
        public readonly AddPaginaManipulador _manipuladorPagina;
        public readonly AddPerfilManipulador _manipuladorPerfil;
        public readonly AddPermissaoManipulador _manipuladorPermissao;
        public readonly DelPermissaoManipulador _manipuladorDelPermissao;
        public AcessoController(IAcessoRepositorio repositorio
            , AddPaginaManipulador manipulador
            , AddPerfilManipulador manipuladorPerfil
            , AddPermissaoManipulador manipuladorPermissao
            , DelPermissaoManipulador manipuladorDelPermissao)
        {
            _repositorio = repositorio;
            _manipuladorPagina = manipulador;
            _manipuladorPerfil = manipuladorPerfil;
            _manipuladorPermissao = manipuladorPermissao;
            _manipuladorDelPermissao = manipuladorDelPermissao;

        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Pagina()
        {
            return await Task.Run(() => View());
        }
        public async Task<IActionResult> ObterPaginas(jQueryDataTableRequestModel request)
        {

            try
            {

                var lista = (await _repositorio.ObterPaginasAsync()).AsQueryable();

                if (request.sSearch != null && request.sSearch.Length > 0)
                {
                    lista = lista.Where(x => x.DesPagina.ToUpper().Contains(request.sSearch.ToUpper()));
                }

                var model = (from r in lista
                             select new
                             {
                                 r.Id,
                                 r.DesPagina,
                                 r.Constante

                             }).DataTableResponse(request);
                return Ok(model);

            }
            catch (System.Exception ex)
            {
                throw;
            }

        }
        public async Task<IActionResult> ObterPerfis(jQueryDataTableRequestModel request)
        {

            try
            {

                var lista = (await _repositorio.ObterPerfisAsync()).AsQueryable();

                if (request.sSearch != null && request.sSearch.Length > 0)
                {
                    lista = lista.Where(x => x.DesPerfil.ToUpper().Contains(request.sSearch.ToUpper()));
                }

                var model = (from r in lista
                             select new
                             {
                                 r.Id,
                                 r.DesPerfil
                             }).DataTableResponse(request);
                return Ok(model);

            }
            catch (System.Exception ex)
            {
                throw;
            }

        }
        [Route("/Admin/Acesso/Perfil/Novo/")]
        [HttpPost]
        //[PermissaoAcesso(1, "Nova", "Post")]
        public async Task<IActionResult> NovoPerfil(AddPerfilComando comando)
        {
            try
            {
                var resultado = await _manipuladorPerfil.ManipuladorAsync(comando);

                return Json(resultado);
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }
        }
        [Route("/Admin/Acesso/Permissao/Excluir/")]
        [HttpPost]
        public async Task<IActionResult> ExcluirPermissao(DelPermissaoComando comando)
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
        [Route("/Admin/Acesso/Pagina/Novo/")]
        [HttpPost]
        //[PermissaoAcesso(1, "Nova", "Post")]
        public async Task<IActionResult> NovaPagina(AddPaginaComando comando)
        {
            try
            {
                var resultado = await _manipuladorPagina.ManipuladorAsync(comando);

                return Json(resultado);
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }
        }
        public async Task<IActionResult> Perfil()
        {
            return await Task.Run(() => View());
        }
        public async Task<IActionResult> DadosAcesso()
        {
            return await Task.Run(() => View());
        }
        public async Task<IActionResult> Permissao()
        {
            var paginas = new SelectList(await _repositorio.ObterPaginasAsync(), "Id", "DesPagina");
            var perfis = new SelectList(await _repositorio.ObterPerfisAsync(), "Id", "DesPerfil");
            ViewBag.Paginas = paginas;
            ViewBag.Perfis = perfis;
            return await Task.Run(() => View());
        }
        [HttpPost]
        //[Route("/Admin/Acesso/Permissao")]
        public async Task<IActionResult> NovaPermissao(AddPermissaoComando comando)
        {
            try
            {
                var resultado = await _manipuladorPermissao.ManipuladorAsync(comando);
                return Json(resultado);
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }
        }
        public async Task<IActionResult> ObterPermissao(string paginaId, string perfilId, jQueryDataTableRequestModel request)
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

            menu.AppendFormat("<a href =\"#\" onclick=ObterValoresLinha(this)  class=\"btn btn-icon fuse-ripple-ready\" title=\"Editar Permissão\"> <i class=\"icon-pencil-lock \"></i>    </a><a href =\"#\" onclick=Excluir(this)  class=\"btn btn-icon fuse-ripple-ready\" title=\"Excluir Permissão\"> <i class=\"icon-delete-forever  \"></i>    </a>", r.PapelId, r.PaginaId);

            return menu.ToString();
        }
    }
}