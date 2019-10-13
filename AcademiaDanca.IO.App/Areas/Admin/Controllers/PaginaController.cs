using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.AcessoComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Acesso;
using AcademiaDanca.IO.Dominio.Contexto.Query.Acesso;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Leanwork.CodePack.DataTables;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaDanca.IO.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PaginaController : Controller
    {
        private readonly IAcessoRepositorio _repositorio;
        public readonly AddPaginaManipulador _manipuladorPagina;
        public readonly DelPaginaManipulador _delManipulador;
        public readonly EditarPaginaManipulador _editarManipulador;
        public PaginaController(IAcessoRepositorio repositorio
            , AddPaginaManipulador manipulador, DelPaginaManipulador delPaginaManipulador, EditarPaginaManipulador editarManipuladorPagina)
        {
            _repositorio = repositorio;
            _manipuladorPagina = manipulador;
            _delManipulador = delPaginaManipulador;
            _editarManipulador = editarManipuladorPagina;

        }
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => View());
        }
        public async Task<IActionResult> Listar(jQueryDataTableRequestModel request)
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
                                 r.Constante,
                                 acao = ObterMenuAcaoDataTable(r)

                             }).DataTableResponse(request);

                return Ok(model);

            }
            catch (System.Exception ex)
            {
                throw;
            }

        }
        public async Task<IActionResult> Criar(AddPaginaComando comando)
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

        public async Task<IActionResult> Editar(EditarPaginaComando comando)
        {
            try
            {
                var resultado = await _editarManipulador.ManipuladorAsync(comando);

                return Json(resultado);
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }
        }

        public async Task<IActionResult> Excluir(DelPaginaComando comando)
        {
            try
            {
                var resultado = await _delManipulador.ManipuladorAsync(comando);

                return Json(resultado);
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }
        }
        private object ObterMenuAcaoDataTable(PaginaResultadoQuery pagina)
        {

            StringBuilder menu = new StringBuilder();

            menu.AppendFormat("<a href =\"#\" onclick=ModalEditar(this)  class=\"btn btn-icon fuse-ripple-ready\" title=\"Editar Permissão\"> <i class=\"icon-pencil-lock \"></i>    </a><a href =\"#\" onclick=Excluir(this)  class=\"btn btn-icon fuse-ripple-ready\" title=\"Excluir Permissão\"> <i class=\"icon-delete-forever  \"></i>    </a>", pagina.Id, pagina.Id);

            return menu.ToString();
        }
    }
}