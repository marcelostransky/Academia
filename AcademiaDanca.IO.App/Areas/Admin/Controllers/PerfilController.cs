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

namespace AcademiaDanca.IO.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [PermissaoAcesso(PaginaId = "PERFIL", Verbo = "Ler")]

    public class PerfilController : Controller
    {
        private readonly IAcessoRepositorio _repositorio;
        public readonly AddPerfilManipulador _manipuladorPerfil;
        public readonly EditarPerfilManipulador _manipuladorEditarPerfil;
        public readonly DelPerfilManipulador _manipuladorDelPerfil;
        public PerfilController(
            IAcessoRepositorio repositorio, 
            AddPerfilManipulador manipuladorPerfil, 
            EditarPerfilManipulador editarMaipulador, 
            DelPerfilManipulador manipuladorDelPerfil)
        {
            _repositorio = repositorio;
            _manipuladorPerfil = manipuladorPerfil;
            _manipuladorEditarPerfil = editarMaipulador;
            _manipuladorDelPerfil = manipuladorDelPerfil;
        }
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => View());
        }
        public async Task<IActionResult> Listar(jQueryDataTableRequestModel request)
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
                                 r.DesPerfil,
                                 acao = ObterMenuAcaoDataTable(r)
                             }).DataTableResponse(request);
                return Ok(model);

            }
            catch (System.Exception ex)
            {
                throw;
            }

        }
        [Route("/Admin/Perfil/Novo/")]
        [HttpPost]
        [PermissaoAcesso(PaginaId = "PERFIL", Verbo = "Criar", TipoRetorno = "Json")]
        public async Task<IActionResult> Criar(AddPerfilComando comando)
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
        [Route("/Admin/Perfil/Editar/")]
        [HttpPut]
        [PermissaoAcesso(PaginaId = "PERFIL", Verbo = "Editar", TipoRetorno = "Json")]

        //[PermissaoAcesso(PaginaId = "Perfil", Verbo = "Editar")]
        public async Task<IActionResult> Editar(EditarPerfilComando comando)
        {
            try
            {
                var resultado = await _manipuladorEditarPerfil.ManipuladorAsync(comando);

                return Json(resultado);
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }
        }
         [Route("/Admin/Perfil/Excluir/")]
         [HttpDelete]
        [PermissaoAcesso(PaginaId = "PERFIL", Verbo = "Excluir", TipoRetorno = "Json")]

        public async Task<IActionResult> Excluir(DelPerfilComando comando)
        {
            try
            {
                var resultado = await _manipuladorDelPerfil.ManipuladorAsync(comando);

                return Json(resultado);
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }
        }
        private object ObterMenuAcaoDataTable(PerfilResultadoQuery r)
        {
            var perfil = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Papel").Value;
            StringBuilder menu = new StringBuilder();
            menu.AppendFormat("<a href =\"#\" onclick=ModalPerfil(this)  class=\"btn btn-icon fuse-ripple-ready\" title=\"Editar Permissão\"> <i class=\"icon-pencil-lock \"></i>    </a><a href =\"#\" onclick=Excluir(this)  class=\"btn btn-icon fuse-ripple-ready\" title=\"Excluir Permissão\"> <i class=\"icon-delete-forever  \"></i>    </a>", r.Id, r.Id);
            return menu.ToString();
        }
    }
}