using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AcademiaDanca.IO.App.Filtros;
using AcademiaDanca.IO.App.Helper;
using AcademiaDanca.IO.App.Models;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.Aluno.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.AlunoComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.AlunoContexto;
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
        private readonly AlunoManipulador _manipulador;
        private readonly EditarFotoAlunoManipulador _manipuladorFoto;
        private readonly AddEnderecoManipulador _manipuladorLogrador;
        private readonly IHostingEnvironment _environment;

        public AlunoController(IAlunoRepositorio repositorio, AlunoManipulador manipulador,
            EditarFotoAlunoManipulador manipuladorFoto,
                  AddEnderecoManipulador manipuladorLogrador,
            IHostingEnvironment environment)
        {
            _repositorio = repositorio;
            _manipulador = manipulador;
            _manipuladorFoto = manipuladorFoto;
            _environment = environment;
            _manipuladorLogrador = manipuladorLogrador;
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
        [Route("/Aluno/Logradouro/Novo")]
        [HttpPost]
        public async Task<IActionResult>Logradouro(AddEnderecoComando comando, int idAluno)
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

    }
}