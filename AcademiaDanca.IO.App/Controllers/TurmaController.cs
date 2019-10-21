using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AcademiaDanca.IO.App.Filtros;
using AcademiaDanca.IO.App.Models;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.TurmaComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.TurmaContexto;
using AcademiaDanca.IO.Dominio.Contexto.Query.Turma;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Leanwork.CodePack.DataTables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AcademiaDanca.IO.App.Controllers
{
    [Authorize]
    [PermissaoAcesso(PaginaId = "TURMA", Verbo = "Ler")]
    public class TurmaController : Controller
    {

        const string _paginaId = "TURMA";
        private readonly ITurmaRepositorio _repositorio;
        private readonly ISalaRepositorio _repositorioSala;
        private readonly IAgendaRepositorio _repositorioAgenda;
        private readonly CriarTurmaManipulador _manipulador;
        private readonly AgendarTurmaManipulador _manipuladorAgenda;
        private readonly ITurmaTipoRepositorio _repositorioTipoTurma;
        private readonly IFuncionarioRepositorio _repositorioFuncionario;
        private readonly DeletarAgendaTurmaManipulador _manipuladorAgendaDeletar;


        public TurmaController(ITurmaRepositorio repositorio,
            CriarTurmaManipulador manipulador,
            ITurmaTipoRepositorio turmatipo,
            IFuncionarioRepositorio repositorioFuncionario,
            ISalaRepositorio repositorioSala,
            IAgendaRepositorio repositorioAgenda,
            AgendarTurmaManipulador manipuladorAgenda,
            DeletarAgendaTurmaManipulador manipuladorAgendaDeletar
            )
        {
            _repositorio = repositorio;
            _manipulador = manipulador;
            _repositorioTipoTurma = turmatipo;
            _repositorioFuncionario = repositorioFuncionario;
            _repositorioSala = repositorioSala;
            _repositorioAgenda = repositorioAgenda;
            _manipuladorAgenda = manipuladorAgenda;
            _manipuladorAgendaDeletar = manipuladorAgendaDeletar;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Calendario(int id)
        {

            var perfil = User.Claims.FirstOrDefault(x => x.Type == "Papel").Value;
            int usuarioId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Sid").Value);
            var turma = (await _repositorio.ObterTodosPorAsync(id, null, null, null, perfil.Equals("Professor") ? usuarioId : 0)).FirstOrDefault();
            var salas = new SelectList(await _repositorioSala.ObterTodosAsync(), "Id", "DesSala");
            var diasSemana = new SelectList(await _repositorioAgenda.ObterTodosDiaSemanaAsync(), "Id", "DiaSemana");
            ViewBag.Turma = turma;
            ViewBag.Dias = diasSemana;
            ViewBag.Salas = salas;
            return View();
        }
        [Route("/Turma/Calendario/Agendamentos")]
        [HttpGet]
        //[PermissaoAcesso(1, "Nova", "Post")]
        public async Task<IActionResult> AgendamentoPorTurma(int idTurma)
        {
            try
            {
                var lista = await _repositorioAgenda.ObterPorAsync(null, idTurma,
                    null, null, null, null, null);
                return Json(new { success = true, agenda = lista });
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }
        }
        [Route("/Turma/Calendario/Agendamento/Deletar")]
        [HttpDelete]
        [PermissaoAcesso(PaginaId = _paginaId, Verbo = "Deletar")]
        public async Task<IActionResult> DeletarAgendamento(int id)
        {
            try
            {
                var data = await _manipuladorAgendaDeletar.ManipuladorAsync(new DeletarAgendaTurmaComando { IdAgenda = id });
                return Json(data);
            }

            catch (System.Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }
        }
        [Route("/Turma/Agendar/Novo")]
        [HttpPost]
        [PermissaoAcesso(PaginaId = _paginaId, Verbo = "Criar")]
        public async Task<IActionResult> Novo(CriarAgendaTurmaComando comando)
        {
            try
            {
                var resultado = await _manipuladorAgenda.ManipuladorAsync(comando);

                return Json(resultado);
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }
        }

        public IActionResult Aluno(int id)
        {
            return View();
        }

        [HttpGet]
        [PermissaoAcesso(PaginaId = _paginaId, Verbo = "Editar")]
        public async Task<IActionResult> Editar(int id)
        {
            var perfil = User.Claims.FirstOrDefault(x => x.Type == "Papel").Value;
            int usuarioId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value);
            var turma = (await _repositorio.ObterTodosPorAsync(id, null, null, null, perfil.Equals("Professor") ? usuarioId : 0)).FirstOrDefault();
            var tipoTurma = (await _repositorioTipoTurma.ObterTodosAsync());
            var professores = (await _repositorioFuncionario.ObterFuncionarioProfessorPorNomeAsync(string.Empty, null));
            ViewBag.TipoTurma = new SelectList(tipoTurma, "Id", "DesTurmaTipo", turma.IdTipoTurma);
            ViewBag.Professores = new SelectList(professores, "IdUsuario", "NomeFuncionario", turma.IdProfessor);
            ViewBag.Turma = turma;
            return View();
        }

        [HttpGet]
        [PermissaoAcesso(PaginaId = _paginaId, Verbo = "Ler")]
        public IActionResult Listar()
        {
            return View();
        }

        [PermissaoAcesso(PaginaId = _paginaId, Verbo = "Criar")]
        public async Task<IActionResult> Nova()
        {
            var tipoTurma = (await _repositorioTipoTurma.ObterTodosAsync());
            var professores = (await _repositorioFuncionario.ObterFuncionarioProfessorPorNomeAsync(string.Empty, null));
            ViewBag.TipoTurma = new SelectList(tipoTurma, "Id", "DesTurmaTipo");
            ViewBag.Professores = new SelectList(professores, "IdUsuario", "NomeFuncionario");
            return View();
        }
        [HttpPost]
        [PermissaoAcesso(PaginaId = _paginaId, Verbo = "Criar", TipoRetorno = "Json")]
        public async Task<IActionResult> Nova(CriarTurmaComando comando)
        {
            try
            {
                var resultado = await _manipulador.ManipuladorAsync(comando);

                return Json(resultado);
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }


        }
        public async Task<IActionResult> Alunos(int idTurma)
        {
            try
            {
                var resultado = await _repositorio.ObterAlunosPorAsync(idTurma);
                return Json(new { success = true, alunos = resultado });
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }


        }
        public async Task<IActionResult> ObterTurmas(FiltroTurmaModel modelFiltro, jQueryDataTableRequestModel request)
        {
            try
            {
                var perfil = User.Claims.FirstOrDefault(x => x.Type == "Papel").Value;
                int usuarioId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value);

                var lista = (await _repositorio.ObterTodosPorAsync(modelFiltro.IdTurma, modelFiltro.IdProfessor, modelFiltro.IdTurmaTipo, null, perfil.Equals("Professor") ? usuarioId : 0)).AsQueryable();

                if (request.sSearch != null && request.sSearch.Length > 0)
                {
                    lista = lista.Where(x => x.DesTurma.ToUpper().Contains(request.sSearch.ToUpper()));
                }

                var model = (from r in lista
                             select new
                             {
                                 r.IdTurma,
                                 Foto = $" <img class=\"rounded img-thumbnail\" style=\" height: 50px;\" src=\"/images/avatars/Funcionario/{r.Foto}\">",
                                 r.NomeProfessor,
                                 r.DesTurma,
                                 r.CodTurma,
                                 r.DesTurmaTipo,
                                 r.Valor,
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

        private object ObterMenuAcaoDataTable(TurmaQueryResultado r)
        {
            var perfil = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Papel").Value;
            StringBuilder menu = new StringBuilder();
            menu.AppendFormat("<a href =\"#\" onclick=ModalCalendario({0}) class=\"btn btn-icon fuse-ripple-ready\" title=\"Calendário\"> <i class=\"icon-calendar-clock\"></i>    </a>", r.IdTurma);
            menu.AppendFormat("<a href =\"#\" onclick=ModalAluno({0})  class=\"btn btn-icon fuse-ripple-ready\" title=\"Aluno\"> <i class=\"icon-account-circle\"></i>    </a>", r.IdTurma);
            menu.AppendFormat("<a href =\"/Turma/Editar/{0}\" target=\"_blank\" class=\"btn btn-icon fuse-ripple-ready\" title=\"Editar\"> <i class=\"icon-border-color \"></i>    </a>", r.IdTurma);
            return menu.ToString();
        }
    }
}