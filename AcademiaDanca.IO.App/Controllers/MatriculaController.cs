using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AcademiaDanca.IO.App.Enums;
using AcademiaDanca.IO.App.Filtros;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada.Com_Matricula;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Financeiro;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Financeiro.FIN_Matricula;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AcademiaDanca.IO.App.Controllers
{
    [Authorize]
    [PermissaoAcesso(PaginaId = "MATR", Verbo = "Ler", TipoRetorno = "Html")]
    public class MatriculaController : Controller
    {
        private readonly IAcessoRepositorio _repositorioAcesso;
        private readonly IMatriculaRepositorio _repositorioMatricula;
        private readonly IConfiguracaoRepositorio _configuracao;
        private readonly IFinanceiroRepositorio _repositorio;
        private readonly IAlunoRepositorio _repositorioAluno;
        private readonly ItemMatriculaManipulador _registrarItemMatriculaManipulador;
        private readonly DelMatriculaItemManipulador _DelItemMatriculaManipulador;
        private readonly ITurmaRepositorio _repositorioTurma;
        public MatriculaController(IFinanceiroRepositorio repositorio
            , ItemMatriculaManipulador itemMatriculaManipulador
            , IAlunoRepositorio repositorioAluno
            , DelMatriculaItemManipulador delItemMatriculaManipulador
            , ITurmaRepositorio turmaRepositorio
            , IMatriculaRepositorio matriculaRepositorio
            , IConfiguracaoRepositorio configuracao
           )
        {
            _repositorio = repositorio;
            _repositorioAluno = repositorioAluno;
            _repositorioTurma = turmaRepositorio;
            _registrarItemMatriculaManipulador = itemMatriculaManipulador;
            _DelItemMatriculaManipulador = delItemMatriculaManipulador;
            _repositorioMatricula = matriculaRepositorio;
            _configuracao = configuracao;
        }

        public IActionResult Matricula(Guid alunoId)
        {
            return View();
        }
        [Route("/Matricula/Aluno/{id}")]
        public async Task<IActionResult> Aluno(Guid id)
        {
            var valorMatricula = $"R${(await _configuracao.ObterPorChaveAsync("Matricula")).Valor}";

            var aluno = await _repositorioAluno.ObterPorAsync(id);
            var perfil = User.Claims.FirstOrDefault(x => x.Type == "Papel").Value;
            int usuarioId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value);
            var lista = Enum.GetValues(typeof(Mes)).Cast<int>().ToList();
            var turmas = await _repositorioTurma.ObterTodosPorAsync(null, null, null, null, perfil.Equals("Professor") ? usuarioId : 0);
            ViewBag.Id = Guid.NewGuid();
            ViewBag.Mes = new SelectList(lista.Select(x => new SelectListItem
            {
                Value = x.ToString(),
                Text = x.ToString()
            }), "Value", "Text");
            ViewBag.Turmas = new SelectList(turmas.Select(x => new SelectListItem
            {
                Value = x.IdTurma.ToString(),
                Text = $"{x.IdTurma}|{ x.CodTurma} | { x.DesTurma} | {x.NomeProfessor} | {Math.Round(x.Valor, 2)} | {x.Ano}"
            }), "Value", "Text");
            ViewBag.Aluno = aluno;
            ViewBag.HashMatricula = Guid.NewGuid();
            ViewBag.ValorMatricula = valorMatricula;
            
            return await Task.Run(() => View());
        }
        [Route("/Matricula/Aluno/Editar/{id}")]
        public async Task<IActionResult> Editar(Guid id)
        {
            var matricula = await _repositorioMatricula.ObterMatriculaCompletoAsync(id);
            var totalGeral = Convert.ToDecimal(0);
            var totalDesconto = Convert.ToDecimal(0);
            if (matricula != null && matricula.MatriculaItens != null && matricula.MatriculaItens.Count > 0)
            {
                totalGeral = matricula.MatriculaItens.Sum(t => t.Valor);
                totalDesconto = matricula.MatriculaItens.Sum(t => t.ValorCalculado);

                var aluno = await _repositorioAluno.ObterPorAsync(id);
                var perfil = User.Claims.FirstOrDefault(x => x.Type == "Papel").Value;
                int usuarioId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value);
                var lista = Enum.GetValues(typeof(Mes)).Cast<int>().ToList();
                var turmas = await _repositorioTurma.ObterTodosPorAsync(null, null, null, null, perfil.Equals("Professor") ? usuarioId : 0);
                ViewBag.Mes = new SelectList(lista.Select(x => new SelectListItem
                {
                    Value = x.ToString(),
                    Text = x.ToString()
                }), "Value", "Text", matricula.MatriculaBase.MatriculaMesInicioPagamento);
                ViewBag.Turmas = new SelectList(turmas.Select(x => new SelectListItem
                {
                    Value = x.IdTurma.ToString(),
                    Text = $"{x.IdTurma}|{ x.CodTurma}|{ x.DesTurma}|{x.NomeProfessor}|{Math.Round(x.Valor, 2)}|{x.Ano}"
                }), "Value", "Text");
                ViewBag.Aluno = aluno;
                ViewBag.HashMatricula = matricula.MatriculaBase.MatriculaGuid;
                ViewBag.Matricula = matricula;
                ViewBag.TotalDesconto = totalDesconto;
            }
            return await Task.Run(() => View());
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