using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AcademiaDanca.IO.App.Filtros;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Financeiro;
using AcademiaDanca.IO.Dominio.Contexto.Query.Financeiro;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Leanwork.CodePack.DataTables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AcademiaDanca.IO.App.Controllers
{
    [Authorize]
    //[PermissaoAcesso(PaginaId = "MENS", Verbo = "Ler", TipoRetorno = "Html")]

    public class FinanceiroController : Controller
    {
        private readonly IAcessoRepositorio _repositorioAcesso;
        private readonly IFinanceiroRepositorio _repositorio;
        private readonly IAlunoRepositorio _repositorioAluno;
        private readonly RegistrarPagamentoMensalidadeManipulador _registrarManipulador;
        private readonly IMensalidadeRepositorio _repositorioMensalidade;

        public FinanceiroController(IFinanceiroRepositorio repositorio,
            IAlunoRepositorio repositorioAluno,
            IMensalidadeRepositorio repositorioMensalidade
            , RegistrarPagamentoMensalidadeManipulador registrarManipulador)
        {
            _repositorio = repositorio;
            _repositorioAluno = repositorioAluno;
            _registrarManipulador = registrarManipulador;
            _repositorioMensalidade = repositorioMensalidade;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _repositorioMensalidade.ObterListaAnoDataVencimento();
            var tipos = await _repositorioMensalidade.ObterTipoMensalidadeAsync();
            ViewBag.Anos = new SelectList(lista.Select(x => new SelectListItem
            {
                Value = x.ToString(),
                Text = x.ToString()
            }), "Value", "Text");
            ViewBag.Tipos = new SelectList(tipos, "Id", "Descricao");
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
        [PermissaoAcesso(PaginaId = "MENS", Verbo = "Ler", TipoRetorno = "json")]
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
        [PermissaoAcesso(PaginaId = "MENS", Verbo = "Ler", TipoRetorno = "Html")]
        public async Task<IActionResult> MensalidadeAsync(Guid? id, string status, int? ano, int? tipo, jQueryDataTableRequestModel request)
        {
            try
            {

                var lista = (await _repositorio.ObterMensalidadesPorAlunoAsync(id, status, ano, tipo)).AsQueryable();

                if (request.sSearch != null && request.sSearch.Length > 0)
                {
                    lista = lista.Where(x => x.AlunoNome.ToUpper().Contains(request.sSearch.ToUpper()));
                }

                var model = (from r in lista
                             select new
                             {
                                 r.AlunoNome,
                                 r.MensalidadeId,
                                 r.TipoMensalidade,
                                 //Foto = $" <img class=\"rounded img-thumbnail\" style=\" height: 50px;\" src=\"/images/avatars/Funcionario/{r.Foto}\">",
                                 r.Parcela,
                                 r.Valor,
                                 total = r.Valor - r.Desconto,
                                 dataVencimento = r.DataVencimento.ToShortDateString(),
                                 r.Desconto,
                                 Pago = (r.Estorno && !r.Pago) ? $"<span class=\"badge badge-warning\"> Estornado - {r.DataEstorno.ToShortDateString()} </ span>" : (r.Pago ? $"<span class=\"badge badge-success\"> Pago - {Convert.ToDateTime(r.DataPagamento).ToShortDateString()} </ span > " : " <span class=\"badge badge-danger\"> Pendente</ span >"),
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

        [PermissaoAcesso(PaginaId = "MENS", Verbo = "Ler", TipoRetorno = "Html")]
        public async Task<IActionResult> MensalidadePorStatusAsync(string status)
        {
            try
            {

                var lista = (await _repositorio.ObterMensalidadesPorAlunoAsync(null, status, null, null));

                return Ok(lista);

            }
            catch (System.Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }
        }

        [PermissaoAcesso(PaginaId = "MENS", Verbo = "Ler", TipoRetorno = "Html")]
        public IActionResult Matricular(Guid id)
        {
            return View();
        }
        private object ObterMenuAcaoDataTable(MensalidadesQueryResultado r)
        {
            var perfil = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Papel").Value;
            StringBuilder menu = new StringBuilder();
            if (!r.Pago)
            {
                menu.AppendFormat("<a href =\"#\" onclick=ModalPagamento(this)  class=\"btn btn-icon fuse-ripple-ready\" title=\"Registrar Pagamento\"> <i class=\"icon-barcode-scan \"></i>    </a>", r.MensalidadeId);
            }
            else
                menu.AppendFormat("<a href =\"#\" onclick=Estornar(this,{0}) class=\"btn btn-icon fuse-ripple-ready\" title=\"Estornar Mensalidade\"> <i class=\"icon-backburger\"></i>    </a>", r.MensalidadeId);

            return menu.ToString();
        }

    }
}