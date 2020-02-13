using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AcademiaDanca.IO.App.Filtros;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.ConfiguracaoComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Configuracao;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaDanca.IO.App.Controllers
{
    [Authorize]
    [PermissaoAcesso(PaginaId = "PARA", Verbo = "Ler", TipoRetorno = "Html")]
    public class ParametroController : Controller
    {
        public readonly IParametroRepositorio _repositorio;
        public readonly EditarParametroManipulador _manipulador;
        public ParametroController(IParametroRepositorio repositorio, EditarParametroManipulador manipulador)
        {
            _repositorio = repositorio;
            _manipulador = manipulador;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Parametros = (await _repositorio.ObterParametrosAsync()).ToList();
            return View();
        }
        [HttpPut]
        public async Task<IActionResult> Editar(ParametroComando comando)
        {
            var resultado = await _manipulador.ManipuladorAsync(comando);
            try
            {
                return Json(resultado);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }
        }
    }
}