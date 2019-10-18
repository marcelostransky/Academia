using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AcademiaDanca.IO.App.Models;
using Microsoft.AspNetCore.Authorization;
using AcademiaDanca.IO.App.Filtros;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using System.Net;

namespace AcademiaDanca.IO.App.Controllers
{
    [Authorize]
   
    public class HomeController : Controller
    {

        private readonly IDashBoardRepositorio _repositorio;
        public HomeController(IDashBoardRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        [PermissaoAcesso(PaginaId = "Dash", Verbo = "Ler", TipoRetorno = "Html")]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Cards()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult NaoAutorizado()
        {
            return View();
        }
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> ObterQuantitativoAlunoMensalidadeAgendamentoAsync()
        {
            var resultado = await _repositorio.ObterQuantitativoAsync();
            try
            {
                return Json(new { success = true, resultado });
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(resultado);
            }
        }
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
