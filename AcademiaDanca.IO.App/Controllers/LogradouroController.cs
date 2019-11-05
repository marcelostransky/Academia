using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.AlunoComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.AlunoContexto;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Financeiro;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaDanca.IO.App.Controllers
{
    public class LogradouroController : Controller
    {
        private readonly AddEnderecoManipulador _manipuladorLogrador;

        public LogradouroController(AddEnderecoManipulador manipuladorLogrador)
        {
            _manipuladorLogrador = manipuladorLogrador;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("/Logradouro/Novo")]
        [HttpPost]
        public async Task<IActionResult> Logradouro(AddEnderecoComando comando, int idAluno)
        {
            var resultado = await _manipuladorLogrador.ManipuladorAsync(comando);
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