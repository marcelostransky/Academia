using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AcademiaDanca.IO.App.Helper;
using AcademiaDanca.IO.App.Models;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FuncionarioComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores;
using AcademiaDanca.IO.Dominio.Contexto.Query;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Leanwork.CodePack.DataTables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AcademiaDanca.IO.App.Controllers
{
    [Authorize]
    public class FuncionarioController : Controller
    {
        private readonly IPerfilRepositorio _repositorioPerfil;
        private readonly IHostingEnvironment _environment;
        private readonly IFuncionarioRepositorio _repositorio;
        private readonly CriarFuncionarioManipulador _manipulador;
        private readonly EditarFuncionarioManipulador _editarManipulador;
        private readonly EditarFotoFuncionarioManipulador _editarFotoManipulador;
        private object hostingEnvironment;

        public FuncionarioController(IFuncionarioRepositorio repositorio,
            IPerfilRepositorio repositorioPerfil,
            CriarFuncionarioManipulador manipulador,
            EditarFuncionarioManipulador editarManipulador,
            EditarFotoFuncionarioManipulador editarFotoManipulador,
            IHostingEnvironment environment
            )
        {
            _repositorio = repositorio;
            _repositorioPerfil = repositorioPerfil;
            _manipulador = manipulador;
            _editarManipulador = editarManipulador;
            _environment = environment;
            _editarFotoManipulador = editarFotoManipulador;
        }
        public async Task<IActionResult> Novo()
        {
            var perfis = (await _repositorioPerfil.ObterTodosAsync());
            ViewBag.Perfis = new SelectList(perfis, "Id", "DescPerfil");
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IComandoResultado> Novo(CriarFuncionarioComando comando)
        {
            try
            {
                comando.Senha = Criptografia.RetornaSenhaCriptografada(comando.Senha);
                comando.ContraSenha = Criptografia.RetornaSenhaCriptografada(comando.ContraSenha);
                var resultado = await _manipulador.ManipuladorAsync(comando);
                return resultado;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
        public async Task<IActionResult> Listar()
        {
            return await Task.Run(() => View());
        }
        [ResponseCache(Duration = 5, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> ObterFuncionario(string nome, jQueryDataTableRequestModel request)
        {
            try
            {
                var lista = (await _repositorio.ObterPorNomeAsync(string.IsNullOrEmpty(nome) ? "" : nome)).AsQueryable();

                if (request.sSearch != null && request.sSearch.Length > 0)
                {
                    lista = lista.Where(x => x.NomeFuncionario.ToUpper().Contains(request.sSearch.ToUpper()));
                }

                var model = (from r in lista
                             select new
                             {
                                 r.IdUsuario,
                                 Foto = $" <img class=\"rounded img-thumbnail\" style=\" height: 50px;\" src=\"/images/avatars/Funcionario/{r.Foto}\">",
                                 r.NomeFuncionario,
                                 r.Email,
                                 r.CPF,
                                 DataNascimento = r.DataNascimento.ToShortDateString(),
                                 r.Perfil,
                                 r.Status,
                                 acao = ObterMenuAcaoDataTable(r)


                             })
                                .DataTableResponse(request);

                return Ok(model);

            }
            catch (System.Exception ex)
            {

                throw;
            }
            //var draw = requestformdata["draw"];
            //dynamic response = new
            //{
            //    Data = lista.ToList(),
            //    Draw = "1",
            //    RecordsFiltered = lista.Count(),
            //    RecordsTotal = lista.Count()
            //};

        }
        public async Task<IActionResult> Editar(int id)
        {
            var funcionario = await _repositorio.ObterPorAsync(id);
            if (funcionario != null)
            {
                var perfis = (await _repositorioPerfil.ObterTodosAsync());
                ViewBag.Perfis = new SelectList(perfis, "Id", "DescPerfil", funcionario.IdPerfil);
            }
            ViewBag.Funcionario = funcionario;

            return View();
        }
        [Route("/Funcionario/Editar/Base")]
        [HttpPost]
        public async Task<IComandoResultado> Editar(EditarBaseFuncionarioComando comando)
        {
            var resultado = await _editarManipulador.ManipuladorAsync(comando);
            return resultado;
        }
        [Route("/Funcionario/Editar/Foto")]
        [HttpPost]
        public async Task<IComandoResultado> Editar([FromForm]FotoModel funcFoto)
        {

            string nomeArquivo = ContentDispositionHeaderValue.Parse(funcFoto.file.ContentDisposition).FileName.Trim('"');
            nomeArquivo = this.VerificarNomeArquivoCorreto(nomeArquivo);
            var extensao = nomeArquivo.Split('.')[1];
            using (FileStream output = System.IO.File.Create(this.ObterCaminhoArquivo($"{funcFoto.Id}.{extensao}")))
            {
                var file = new FileInfo(output.Name);
                await funcFoto.file.CopyToAsync(output);
            }
            EditarFotoFuncionarioComando comando = new EditarFotoFuncionarioComando();
            comando.Id = funcFoto.Id;
            comando.Foto = $"{funcFoto.Id}.{extensao}";
            var resultado = await _editarFotoManipulador.ManipuladorAsync(comando);
            return resultado;
        }
        private string VerificarNomeArquivoCorreto(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

        private string ObterCaminhoArquivo(string nomeArquivo)
        {
            return _environment.WebRootPath + $"\\images\\avatars\\Funcionario\\{nomeArquivo}";
        }
        private object ObterMenuAcaoDataTable(FuncioanrioQueryPorNomeResultado r)
        {
            //var papel = Util.RolesUser(2);
            StringBuilder menu = new StringBuilder();
            menu.AppendFormat("<a href =\"/Funcionario/Editar/{0}\" class=\"btn btn-icon fuse-ripple-ready\" title=\"Editar Professor\"> <i class=\"icon-account-edit\"></i>    </a>", r.IdUsuario);
            if (r.Perfil.Equals("Professor"))
            {
                menu.AppendFormat("<a href =\"/AprovaBrasil/Avaliacao/Estatistica/{0}\" class=\"btn btn-icon fuse-ripple-ready \" title=\"Turmas\"> <i class=\"icon-school \"></i>    </a>", r.IdUsuario);

            }
            return menu.ToString();
        }

    }
}