using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AcademiaDanca.IO.App.Filtros;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Financeiro;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaDanca.IO.App.Controllers
{
    [Authorize]
    public class MensalidadeController : Controller
    {
        private readonly IAcessoRepositorio _repositorioAcesso;
        private readonly IFinanceiroRepositorio _repositorio;
        private readonly IAlunoRepositorio _repositorioAluno;
        private readonly RegistrarPagamentoMensalidadeManipulador _registrarManipulador;
        public MensalidadeController(IFinanceiroRepositorio repositorio,
            IAlunoRepositorio repositorioAluno
            , RegistrarPagamentoMensalidadeManipulador registrarManipulador)
        {
            _repositorio = repositorio;
            _repositorioAluno = repositorioAluno;
            _registrarManipulador = registrarManipulador;
        }
        public IActionResult Index()
        {
            return View();
        }
        [PermissaoAcesso(PaginaId = "MENS", Verbo = "Criar", TipoRetorno = "json")]
        public async Task<IActionResult> RegistrarPagamento(RegistrarPagamentoMensalidadeComando comando)
        {
            var resultado = await _registrarManipulador.ManipuladorAsync(comando);
            try
            {
                return Json(resultado);
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(resultado);
            }

        }
        
    }
}