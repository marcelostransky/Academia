﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AcademiaDanca.IO.App.Enums;
using AcademiaDanca.IO.App.Filtros;
using AcademiaDanca.IO.App.Helper;
using AcademiaDanca.IO.App.Models;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.Aluno.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.AlunoComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.AlunoContexto;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Financeiro;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AcademiaDanca.IO.App.Controllers
{
    [Authorize]
    [PermissaoAcesso(1, null, null)]
    public class AlunoController : Controller
    {
        private readonly IAlunoRepositorio _repositorio;
        private readonly ITurmaRepositorio _repositorioTurma;
        private readonly AlunoManipulador _manipulador;
        private readonly EditarFotoAlunoManipulador _manipuladorFoto;
        private readonly AddEnderecoManipulador _manipuladorLogrador;
        private readonly AddTurmaAlunoManipulador _manipuladorAlunoTurma;
        private readonly AddResponsavelManipulador _manipuladorResponsavel;
        private readonly DelTurmaAlunoManipulador _manipuladorDelTurmaAluno;
        private readonly MatricularManipulador _manipuladorMatricula;
        private readonly IHostingEnvironment _environment;

        public AlunoController(
            IAlunoRepositorio repositorio,
            ITurmaRepositorio turmaRepositorio,
            AlunoManipulador manipulador,
            EditarFotoAlunoManipulador manipuladorFoto,
            AddEnderecoManipulador manipuladorLogrador,
            AddResponsavelManipulador manipuladorResponsavel,
            AddTurmaAlunoManipulador manipuladorAlunoTurma,
            DelTurmaAlunoManipulador manipuladorDelTurmaAluno,
            MatricularManipulador matricularManipulador,
            IHostingEnvironment environment)
        {
            _repositorio = repositorio;
            _repositorioTurma = turmaRepositorio;
            _manipulador = manipulador;
            _manipuladorFoto = manipuladorFoto;
            _environment = environment;
            _manipuladorLogrador = manipuladorLogrador;
            _manipuladorResponsavel = manipuladorResponsavel;
            _manipuladorAlunoTurma = manipuladorAlunoTurma;
            _manipuladorDelTurmaAluno = manipuladorDelTurmaAluno;
            _manipuladorMatricula = matricularManipulador;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Novo()
        {
            var lista = Enum.GetValues(typeof(Mes)).Cast<int>().ToList();
            var turmas = await _repositorioTurma.ObterTodosPorAsync(null, null, null, null);
            ViewBag.Id = Guid.Parse("b8a35e32-5e59-4ce3-92f7-77caf6a5e0e2");
            var selectListItems = (await new EstadoModel().ObterListaUF()).Select(x => new SelectListItem() { Text = x, Value = x });
            ViewBag.Estados = new SelectList(selectListItems, "Value", "Text");
            ViewBag.TipoFiliacao = new SelectList(await _repositorio.ObterTipoFiliacaoAsync(), "Id", "Nome");
            ViewBag.Mes = new SelectList(lista.Select(x => new SelectListItem
            {
                Value = x.ToString(),
                Text = x.ToString()
            }), "Value", "Text");
            ViewBag.Turmas = new SelectList(turmas.Select(x => new SelectListItem
            {
                Value = x.IdTurma.ToString(),
                Text = $"{ x.CodTurma} | { x.DesTurma} | {x.NomeProfessor} | {Math.Round(x.Valor, 2)} | {x.Ano}"
            }), "Value", "Text");
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
        [Route("/Aluno/TotalTurmas")]
        public async Task<IActionResult> TotalTurmas(Guid alunoId)
        {
            try
            {
                var resultado = await _repositorio.ObterTotalTurmaAsync(alunoId);
                return Json(resultado);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }
        }
        [Route("/Aluno/Turma/Novo")]
        [HttpPost]
        public async Task<IActionResult> NovaTurma(AddTurmaComando comando)
        {
            try
            {
                var resultado = await _manipuladorAlunoTurma.ManipuladorAsync(comando);
                return Json(resultado);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }

        }
        [Route("/Aluno/Turma/Deletar")]
        [HttpDelete]
        public async Task<IActionResult> DeletarTurma(DelTurmaAlunoComando comando)
        {
            try
            {
                var resultado = await _manipuladorDelTurmaAluno.ManipuladorAsync(comando);
                return Json(resultado);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }

        }
        [Route("/Aluno/Logradouro/Novo")]
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
        [Route("/Aluno/Responsavel/Novo")]
        public async Task<IActionResult> Responsavel(AddFiliacaoComando comando)
        {
            var resultado = await _manipuladorResponsavel.ManipuladorAsync(comando);
            if (resultado.Success)
                return Json(resultado);
            else
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(resultado);
            }
        }
        [Route("/Aluno/Editar/Foto")]
        [HttpPost]
        public async Task<IComandoResultado> Editar([FromForm]FotoModel funcFoto)
        {
            var util = new Util(_environment);
            string nomeArquivo = ContentDispositionHeaderValue.Parse(funcFoto.file.ContentDisposition).FileName.Trim('"');
            nomeArquivo = util.VerificarNomeArquivoCorreto(nomeArquivo);
            var extensao = nomeArquivo.Split('.')[1];
            using (FileStream output = System.IO.File.Create(util.ObterCaminhoArquivo($"{funcFoto.Id}.{extensao}", "Aluno")))
            {
                var file = new FileInfo(output.Name);
                await funcFoto.file.CopyToAsync(output);
            }
            EditarFotoAlunoComando comando = new EditarFotoAlunoComando();
            comando.Id = funcFoto.Id;
            comando.Foto = $"{funcFoto.Id}.{extensao}";
            var resultado = await _manipuladorFoto.ManipuladorAsync(comando);
            return resultado;
        }
        [Route("/Aluno/Matricular")]
        [HttpPost]
        public async Task<IActionResult> Matricular(MatricularComando comando)
        {
            var resultado = await _manipuladorMatricula.ManipuladorAsync(comando);
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