using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AcademiaDanca.IO.Dominio.Contexto.Query.Financeiro;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Leanwork.CodePack.DataTables;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaDanca.IO.App.Controllers
{
    public class FinanceiroController : Controller
    {
        private readonly IFinanceiroRepositorio _repositorio;
        private readonly IAlunoRepositorio _repositorioAluno;
        public FinanceiroController(IFinanceiroRepositorio repositorio, IAlunoRepositorio repositorioAluno)
        {
            _repositorio = repositorio;
            _repositorioAluno = repositorioAluno;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Alunos(string nome)
        {
            try
            {
                var resultado = await _repositorioAluno.ObterTodosPorAsync(nome);
                return Json(new { success = true, alunos = resultado });
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }


        }
        public async Task<IActionResult> MensalidadeAsync(Guid? id, string status, jQueryDataTableRequestModel request)
        {
            try
            {
                var t = status;
                var lista = (await _repositorio.ObterMensalidadesPorAlunoAsync(id, status)).AsQueryable();

                if (request.sSearch != null && request.sSearch.Length > 0)
                {
                    lista = lista.Where(x => x.AlunoNome.ToUpper().Contains(request.sSearch.ToUpper()));
                }

                var model = (from r in lista
                             select new
                             {
                                 r.AlunoNome,
                                 //Foto = $" <img class=\"rounded img-thumbnail\" style=\" height: 50px;\" src=\"/images/avatars/Funcionario/{r.Foto}\">",
                                 r.Parcela,
                                 r.Valor,
                                 total = r.Valor - r.Desconto,
                                 dataVencimento = r.DataVencimento.ToShortDateString(),
                                 r.Desconto,
                                 Pago = r.Pago ? $"<span class=\"badge badge-success\"> Pago - {r.DataVencimento.ToShortDateString()} </ span > " : " <span class=\"badge badge-danger\"> Pendente</ span >",
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
        public IActionResult Matricular(Guid id)
        {
            return View();
        }
        private object ObterMenuAcaoDataTable(MensalidadesQueryResultado r)
        {
            var perfil = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Papel").Value;
            StringBuilder menu = new StringBuilder();
            menu.AppendFormat("<a href =\"#\" onclick=ModalCalendario({0}) class=\"btn btn-icon fuse-ripple-ready\" title=\"Calendário\"> <i class=\"icon-calendar-clock\"></i>    </a>", r.MensalidadeId);
            menu.AppendFormat("<a href =\"#\" onclick=ModalAluno({0})  class=\"btn btn-icon fuse-ripple-ready\" title=\"Aluno\"> <i class=\"icon-account-circle\"></i>    </a>", r.MensalidadeId);
            menu.AppendFormat("<a href =\"/Turma/Editar/{0}\" target=\"_blank\" class=\"btn btn-icon fuse-ripple-ready\" title=\"Editar\"> <i class=\"icon-border-color \"></i>    </a>", r.MensalidadeId);
            return menu.ToString();
        }

    }
}