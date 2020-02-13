using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AcademiaDanca.IO.App.Filtros;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada.Com_Mensalidade;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Financeiro;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Financeiro.Fin_Mensalidade;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AcademiaDanca.IO.App.Controllers
{
    [Authorize]
    public class MensalidadeController : Controller
    {
        private readonly IAcessoRepositorio _repositorioAcesso;
        private readonly IFinanceiroRepositorio _repositorio;
        private readonly IAlunoRepositorio _repositorioAluno;
        private readonly IMensalidadeRepositorio _repositorioMensalidade;
        private readonly RegistrarPagamentoMensalidadeManipulador _registrarManipulador;
        private readonly EstornarMensalidadeManipulador _estornarManipulador;
        public MensalidadeController(IFinanceiroRepositorio repositorio,
            IAlunoRepositorio repositorioAluno
            , RegistrarPagamentoMensalidadeManipulador registrarManipulador
            , IMensalidadeRepositorio mensalidadeRepositorio
            , EstornarMensalidadeManipulador estornarMensalidadeManipulador)
        {
            _repositorio = repositorio;
            _repositorioAluno = repositorioAluno;
            _registrarManipulador = registrarManipulador;
            _repositorioMensalidade = mensalidadeRepositorio;
            _estornarManipulador = estornarMensalidadeManipulador;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Route("/Financeiro/EsperadoRealizado")]
        public async Task<IActionResult> EsperadoRealizado()
        {
            var lista = await _repositorioMensalidade.ObterListaAnoDataVencimento();
            ViewBag.Anos = new SelectList(lista.Select(x => new SelectListItem
            {
                Value = x.ToString(),
                Text = x.ToString()
            }), "Value", "Text", DateTime.Now.Year);
            return await Task.Run(() => View());
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
        [HttpPut]
        [Route("/Mensalidade/Estornar")]
        [PermissaoAcesso(PaginaId = "MENS", Verbo = "Editar", TipoRetorno = "json")]
        public async Task<IActionResult> EstornarPagamento(EstornarMensalidadeComando comando)
        {
            comando.IdUsuario =Convert.ToInt32( User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value);
            var resultado = await _estornarManipulador.ManipuladorAsync(comando);
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