using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AcademiaDanca.IO.App.Filtros;
using AcademiaDanca.IO.App.Helper;
using AcademiaDanca.IO.App.Models;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.Aluno.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.AlunoContexto;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AcademiaDanca.IO.App.Controllers
{
    [Authorize]
    [PermissaoAcesso(1, null, null)]
    public class AlunoController : Controller
    {
        private readonly IAlunoRepositorio _repositorio;
        private readonly AlunoManipulador _manipulador;

        public AlunoController(IAlunoRepositorio repositorio, AlunoManipulador manipulador)
        {
            _repositorio = repositorio;
            _manipulador = manipulador;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Novo()
        {
            ViewBag.Id = Guid.NewGuid();
            var selectListItems = (await new EstadoModel().ObterListaUF()).Select(x => new SelectListItem() { Text = x, Value = x });
            ViewBag.Estados = new SelectList(selectListItems, "Value", "Text");
            return View();
        }
        public IActionResult ObterLogradouroPor(string cep)
        {
            var cepretorno = new Cep();
            return Json(new { success = true, CEP = cepretorno.ObterLogradouroAsync(cep) });
        }
        public IActionResult Listar()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Novo(CriarAlunoComando comando)
        {
            var resultado = await _manipulador.ManipuladorAsync(comando);
            if (resultado.Success)
                return Json(resultado);
            else
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(resultado);
            }
        }
    }
}