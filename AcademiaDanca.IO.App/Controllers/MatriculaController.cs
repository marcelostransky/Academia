using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AcademiaDanca.IO.App.Filtros;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada.Com_Matricula;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Financeiro;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Financeiro.FIN_Matricula;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaDanca.IO.App.Controllers
{
    [Authorize]
    [PermissaoAcesso(PaginaId = "MATR", Verbo = "Ler", TipoRetorno = "Html")]

    public class MatriculaController : Controller
    {
        private readonly IAcessoRepositorio _repositorioAcesso;
        private readonly IFinanceiroRepositorio _repositorio;
        private readonly IAlunoRepositorio _repositorioAluno;
        private readonly ItemMatriculaManipulador _registrarItemMatriculaManipulador;
        private readonly DelMatriculaItemManipulador _DelItemMatriculaManipulador;

        public MatriculaController(IFinanceiroRepositorio repositorio, ItemMatriculaManipulador itemMatriculaManipulador
           ,IAlunoRepositorio repositorioAluno
            , DelMatriculaItemManipulador  delItemMatriculaManipulador
           )
        {
            _repositorio = repositorio;
            _repositorioAluno = repositorioAluno;
            _registrarItemMatriculaManipulador = itemMatriculaManipulador;
            _DelItemMatriculaManipulador = delItemMatriculaManipulador;

        }

        public IActionResult Matricula(Guid alunoId)
        {
            return View();
        }

        [Route("/Matricula/Item/{id}")]
        public async Task<IActionResult> ItemAsync(int id)
        {
            var resultado = await _repositorio.ObterItensMatriculaPor(id);
            var totalGeral = resultado.Sum(t => t.Valor);
            var totalDesconto = resultado.Sum(t => t.ValorCalculado);


            try
            {
                return Json(new { msg = "OK", itens = resultado, totalGeral, totalDesconto });
            }
            catch (Exception ex)
            {

                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }
        }
        [Route("/Matricula/Item/Add")]
        [HttpPost]
        public async Task<IActionResult> AddItemAsync(MatriculaItemComando comando)
        {
            

            var resultado = await _registrarItemMatriculaManipulador.ManipuladorAsync(comando);
            
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
        [Route("/Matricula/Item/Del")]
        [HttpDelete]
        public async Task<IActionResult> DelItemAsync(DelMatriculaItemComando comando)
        {
            var resultado = await _DelItemMatriculaManipulador.ManipuladorAsync(comando);
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