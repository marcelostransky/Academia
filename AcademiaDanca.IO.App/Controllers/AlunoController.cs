﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AcademiaDanca.IO.App.Enums;
using AcademiaDanca.IO.App.Filtros;
using AcademiaDanca.IO.App.Helper;
using AcademiaDanca.IO.App.Models;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.Aluno.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.AlunoComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada.Com_Matricula;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Aluno;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Financeiro.FIN_Matricula;
using AcademiaDanca.IO.Dominio.Contexto.Query.Aluno;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Leanwork.CodePack.DataTables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AcademiaDanca.IO.App.Controllers
{
    [Authorize]
    //[PermissaoAcesso(PaginaId = "ALUNO", Verbo = "Ler", TipoRetorno = "Html")]
    public class AlunoController : Controller
    {
        public readonly IAcessoRepositorio _repositorioAcesso;
        private readonly IAlunoRepositorio _repositorio;
        private readonly ITurmaRepositorio _repositorioTurma;
        private readonly AlunoManipulador _manipulador;
        private readonly EditarFotoAlunoManipulador _manipuladorFoto;
        private readonly EditarAlunoManipulador _manipuladorEditAluno;
        private readonly AddTurmaAlunoManipulador _manipuladorAlunoTurma;
        private readonly AddResponsavelManipulador _manipuladorResponsavel;
        private readonly DelTurmaAlunoManipulador _manipuladorDelTurmaAluno;
        private readonly MatricularManipulador _manipuladorMatricula;
        private readonly IHostingEnvironment _environment;
        public const int _codPagina = 10;

        public AlunoController(
            IAlunoRepositorio repositorio,
            ITurmaRepositorio turmaRepositorio,
            AlunoManipulador manipulador,
            EditarAlunoManipulador manipuladorEditAluno,
            EditarFotoAlunoManipulador manipuladorFoto,
            AddResponsavelManipulador manipuladorResponsavel,
            AddTurmaAlunoManipulador manipuladorAlunoTurma,
            DelTurmaAlunoManipulador manipuladorDelTurmaAluno,
            MatricularManipulador matricularManipulador,
            IHostingEnvironment environment, IAcessoRepositorio repositorioAcesso)
        {
            _repositorio = repositorio;
            _repositorioTurma = turmaRepositorio;
            _manipulador = manipulador;
            _manipuladorFoto = manipuladorFoto;
            _environment = environment;
            _manipuladorResponsavel = manipuladorResponsavel;
            _manipuladorAlunoTurma = manipuladorAlunoTurma;
            _manipuladorDelTurmaAluno = manipuladorDelTurmaAluno;
            _manipuladorMatricula = matricularManipulador;
            _repositorioAcesso = repositorioAcesso;
            _manipuladorEditAluno = manipuladorEditAluno;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Detalhar(Guid id)
        {
            var aluno = await _repositorio.ObterAlunoCompletoAsync(id);
            //aluno.AlunoMensalidades = aluno.AlunoMensalidades.OrderBy(x => x.MensalidadeParcela).ToList();
            //aluno.AlunoMensalidades[0].MensalidadeDataPagamento.ToShortDateString()
            var total = 0;
            if (aluno != null)
            {
                total = aluno.AlunoTurmas.Count;
            }

            ViewBag.Aluno = aluno;
            return View();
        }
        [PermissaoAcesso(PaginaId = "ALUNO", Verbo = "Editar", TipoRetorno = "Html")]
        public async Task<IActionResult> Editar(Guid id)
        {
            var Aluno = await _repositorio.ObterAlunoCompletoAsync(id);
            var selectListItems = (await new EstadoModel().ObterListaUF()).Select(x => new SelectListItem() { Text = x, Value = x });
            ViewBag.Estados = new SelectList(selectListItems, "Value", "Text", Aluno.AlunoLogradouro.LogradouroEstado);
            ViewBag.TipoFiliacao = new SelectList(await _repositorio.ObterTipoFiliacaoAsync(), "Id", "Nome");
            ViewBag.Aluno = Aluno;
            return View();
        }
        [Route("/Aluno/Editar/")]
        [HttpPut]
        [PermissaoAcesso(PaginaId = "ALUNO", Verbo = "Editar", TipoRetorno = "Json")]
        public async Task<IActionResult> Editar(EditarAlunoComando comando)
        {
            var resultado = await _manipuladorEditAluno.ManipuladorAsync(comando);
            if (resultado.Success)
                return Json(resultado);
            else
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(resultado);
            }

        }
        [Route("/Aluno/Editar/EditarFoto")]
        [PermissaoAcesso(PaginaId = "ALUNO", Verbo = "Editar", TipoRetorno = "Json")]
        [HttpPost]
        public async Task<IActionResult> EditarFoto([FromForm]FotoModel funcFoto)
        {
            try
            {

                var util = new Util(_environment);
                EditarFotoAlunoComando comando = new EditarFotoAlunoComando();
                if (funcFoto.file.Length > 0)
                {
                    string nomeArquivo = ContentDispositionHeaderValue.Parse(funcFoto.file.ContentDisposition).FileName.Trim('"');

                    nomeArquivo = util.VerificarNomeArquivoCorreto(nomeArquivo);
                    var extensao = nomeArquivo.Split('.')[1];
                    using (FileStream output = System.IO.File.Create(util.ObterCaminhoArquivo($"{funcFoto.Id}.{extensao}", "Aluno")))
                    {
                        var file = new FileInfo(output.Name);
                        await funcFoto.file.CopyToAsync(output);
                    }

                    comando.Id = funcFoto.Id;
                    comando.Foto = $"{funcFoto.Id}.{extensao}";
                }

                var resultado = await _manipuladorFoto.ManipuladorAsync(comando);
                return Json(resultado);
            }
            catch (Exception ex)
            {

                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }

        }
        public IActionResult Consultar()
        {
            return View();
        }
        [PermissaoAcesso(PaginaId = "ALUNO", Verbo = "Criar", TipoRetorno = "Html")]
        public async Task<IActionResult> Novo()
        {
            var perfil = User.Claims.FirstOrDefault(x => x.Type == "Papel").Value;
            int usuarioId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value);
            var lista = Enum.GetValues(typeof(Mes)).Cast<int>().ToList();
            var turmas = await _repositorioTurma.ObterTodosPorAsync(null, null, null, null, perfil.Equals("Professor") ? usuarioId : 0);
            ViewBag.Id = Guid.NewGuid();
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
                Text = $"{x.IdTurma}|{ x.CodTurma} | { x.DesTurma} | {x.NomeProfessor} | {Math.Round(x.Valor, 2)} | {x.Ano}"
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
        [PermissaoAcesso(PaginaId = "ALUNO", Verbo = "Criar", TipoRetorno = "json")]
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
            EditarFotoAlunoComando comando = new EditarFotoAlunoComando();
            var util = new Util(_environment);
            if (!string.IsNullOrWhiteSpace(funcFoto.base64image))
            {
                byte[] arquivoB64 = Convert.FromBase64String(funcFoto.base64image.Replace("data:image/jpeg;base64,", ""));
                var diretorio = util.ObterCaminhoArquivo($"{funcFoto.Id}.jpg", "Aluno");
                System.IO.File.WriteAllBytes($"{diretorio}", arquivoB64);
                comando.Id = funcFoto.Id;
                comando.Foto = $"{funcFoto.Id}.jpg";

            }
            else if (funcFoto.file.Length > 0)
            {
                string nomeArquivo = ContentDispositionHeaderValue.Parse(funcFoto.file.ContentDisposition).FileName.Trim('"');
                nomeArquivo = util.VerificarNomeArquivoCorreto(nomeArquivo);
                var extensao = nomeArquivo.Split('.')[1];
                using (FileStream output = System.IO.File.Create(util.ObterCaminhoArquivo($"{funcFoto.Id}.{extensao}", "Aluno")))
                {
                    var file = new FileInfo(output.Name);
                    await funcFoto.file.CopyToAsync(output);
                }
                comando.Id = funcFoto.Id;
                comando.Foto = $"{funcFoto.Id}.{extensao}";
            }
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
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }

        }

        public async Task<IActionResult> ObterAlunos(string nome, jQueryDataTableRequestModel request)
        {
            try
            {
                var lista = (await _repositorio.ObterTodosPorAsync(nome)).OrderBy(X => X.AlunoNome).AsQueryable();

                if (request.sSearch != null && request.sSearch.Length > 0)
                {
                    lista = lista.Where(x => x.AlunoNome.ToUpper().Contains(request.sSearch.ToUpper()));
                }

                var model = (from r in lista
                             select new
                             {
                                 r.AlunoId,
                                 Foto = $" <img class=\"rounded img-thumbnail\" style=\" height: 50px;\" src=\"/images/avatars/Aluno/{r.AlunoFoto}\">",
                                 r.AlunoNome,
                                 r.AlunoTelefone,
                                 DataNascimento = r.DataNascimento.ToShortDateString(),
                                 StatusMatricula = (r.AlunoStatusMatricula) ? $" <img class=\"rounded img-thumbnail\" alt=\"Inativo\" style=\" height: 20px;\" src=\"/images/backgrounds/verde.png\">" : $" <img class=\"rounded img-thumbnail\" alt=\"Inativo\" style=\" height: 20px;\" src=\"/images/backgrounds/vermelho.png\">",
                                 acao = ObterMenuAcaoDataTable(r)

                             })
                             .DataTableResponse(request);

                return Ok(model);

            }
            catch (System.Exception ex)
            {
                throw;
            }

        }

        private object ObterMenuAcaoDataTable(AlunoPorNomeQuery r)
        {
            //var perfil = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Papel").Value;
            StringBuilder menu = new StringBuilder();
            if (!r.AlunoStatusMatricula)
                menu.AppendFormat("<a href =\"/Matricula/Aluno/{0}\" target=\"_blank\" class=\"btn btn-icon fuse-ripple-ready\" title=\"Matricila\"> <i class=\"icon-content-paste\"></i>    </a>", r.AlunoUifId);
            else
                menu.AppendFormat("<a href =\"/Matricula/Aluno/Editar/{0}\" target=\"_blank\" class=\"btn btn-icon fuse-ripple-ready\" title=\"Matricila\"> <i class=\"icon-content-paste\"></i>    </a>", r.AlunoUifId);

            //menu.AppendFormat("<a href =\"/Financeiro/Mensalidade/{0}\" target=\"_blank\" class=\"btn btn-icon fuse-ripple-ready\" title=\"Mensalidade\"> <i class=\"icon-cash-usd\"></i>    </a>", r.UifId);
            menu.AppendFormat("<a href =\"/Aluno/Detalhar/{0}\" target=\"_blank\" class=\"btn btn-icon fuse-ripple-ready\" title=\"Detalhar Aluno\"> <i class=\"icon-account-circle\"></i>    </a>", r.AlunoUifId);
            menu.AppendFormat("<a href =\"/Aluno/Editar/{0}\" target=\"_blank\" class=\"btn btn-icon fuse-ripple-ready\" title=\"Editar Aluno\"> <i class=\"icon-border-color\"></i>    </a>", r.AlunoUifId);
            menu.AppendFormat("<a href =\"/Aluno/Excluir/{0}\" target=\"_blank\" class=\"btn btn-icon fuse-ripple-ready\" title=\"Excluir Aluno\"> <i class=\"icon-delete-forever\"></i>    </a>", r.AlunoUifId);
            return menu.ToString();
        }
    }
}