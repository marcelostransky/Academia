using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AcademiaDanca.IO.App.Filtros;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.Acesso.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Acesso;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Leanwork.CodePack.DataTables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaDanca.IO.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AcessoController : Controller
    {
        private readonly IAcessoRepositorio _repositorio;
        public readonly AddPaginaManipulador _manipuladorPagina;
        public readonly AddPerfilManipulador _manipuladorPerfil;
        public AcessoController(IAcessoRepositorio repositorio
            ,AddPaginaManipulador manipulador
            ,AddPerfilManipulador manipuladorPerfil)
        {
            _repositorio = repositorio;
            _manipuladorPagina = manipulador;
            _manipuladorPerfil = manipuladorPerfil;

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
                                 r.DesPagina
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
            return await Task.Run(() => View());
        }
    }
}