using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AcademiaDanca.IO.App.Helper;
using AcademiaDanca.IO.Dominio.Contexto.Query.Aluno;
using AcademiaDanca.IO.Dominio.Contexto.Query.Financeiro.Mensalidade;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Leanwork.CodePack.DataTables;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace AcademiaDanca.IO.App.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly IRelatorioRepositorio _repositorio;
        private readonly IAlunoRepositorio _repositorioAluno;
        private readonly IHostingEnvironment _hostingEnvironment;
        public RelatorioController(IRelatorioRepositorio repositorio,
            IHostingEnvironment host,
            IAlunoRepositorio repositorioAluno)
        {
            _repositorio = repositorio;
            _hostingEnvironment = host;
            _repositorioAluno = repositorioAluno;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("/Relatorio/Devedor")]
        public async Task<IActionResult> DevedorAsync()
        {
            return await Task.Run(() => View());
        }
        [Route("/Financeiro/EsperadoRealizado/Grafico")]
        public async Task<IActionResult> EsperadoRealizado(int ano)
        {
            try
            {
                var resultado = await _repositorio.ObterMensalidadesEsperadoRealizadoAsync(null, ano);
                return Json(resultado);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }

        }
        [Route("/relatorio/listar/aluno")]
        public async Task<IActionResult> ListarAlunosAsync()
        {
            return await Task.Run(() => View());
        }
        public async Task<IActionResult> ListarMensalidadesVencidas(int? idMatricula, int? idMes, int? idAluno, jQueryDataTableRequestModel request)
        {

            try
            {

                var lista = (await _repositorio.ObterMensalidadesVencidasAsync(idMatricula, idMes, idAluno)).AsQueryable();

                if (request.sSearch != null && request.sSearch.Length > 0)
                {
                    lista = lista.Where(x => x.AlunoNome.ToUpper().Contains(request.sSearch.ToUpper()));
                }

                var model = (from r in lista
                             select new
                             {
                                 r.AlunoId,
                                 r.AlunoNome,
                                 r.MatriculaId,
                                 r.MensalidadeId,
                                 r.MensalidadeParcela,
                                 data = r.MesalidadeDataVencimento.ToShortDateString(),
                                 valor = $"R$ {r.MensalidadeValor}",
                                 r.MensalidadeTotalAtraso,
                                 r.TipoMensalidadeDescricao

                                 //acao = ObterMenuAcaoDataTable(r)
                             }).DataTableResponse(request);
                return Ok(model);

            }
            catch (System.Exception ex)
            {
                throw;
            }

        }

        public async Task<IActionResult> ListarAlunos(bool? status, jQueryDataTableRequestModel request)
        {
            try
            {

                var lista = (await _repositorioAluno.ObterTodosPorAsync(status)).OrderBy(x => x.AlunoNome).AsQueryable();

                if (request.sSearch != null && request.sSearch.Length > 0)
                {
                    lista = lista.Where(x => x.AlunoNome.ToUpper().Contains(request.sSearch.ToUpper()));
                }

                var model = (from r in lista
                             select new
                             {
                                 Codigo = r.AlunoId,
                                 Aluno = r.AlunoNome,
                                 AlunoStatusMatricula = r.AlunoStatusMatricula ? "Sim" : "Não",
                                 r.AlunoEmail,
                                 r.AlunoTelefone,
                                 r.AlunoCelular,
                                 data = r.AlunoDataToShortDate,
                                 Foto = $" <img class=\"rounded img-thumbnail\" style=\" height: 40px;\" src=\"/images/avatars/Aluno/{r.AlunoFoto}\">",


                                 //acao = ObterMenuAcaoDataTable(r)
                             }).DataTableResponse(request);
                return Ok(model);

            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        [Route("Export/Excel/Pendentes")]
        public async Task<IActionResult> ExportListaPendenteExcelAsync()
        {
            string rootFolder = $"{ _hostingEnvironment.WebRootPath}\\xls";
            string fileName = $"{Guid.NewGuid()}.xlsx";
            var util = new Util(_hostingEnvironment);
            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));
            string[] colunas = new string[] {
                "Matricula ID",
                "Matricula Status",
                "Matricula Ano" ,
                "Aluno Id",
                "Aluno",
                "Tipo Mensalidade",
                "Mensalidade ID",
                "Data Vencimento",
                "Valor",
                "Parcela"
                ,"Mes",
                "Ano",
                "Total de dias de atraso"};
            List<MensalidadeVencidaQueryResultado> list = (await _repositorio.ObterMensalidadesVencidasAsync(null, null, null)).ToList();
            Dictionary<int, string[]> linhas = new Dictionary<int, string[]>();
            int j = 0;
            foreach (var item in list)
            {
                linhas.Add(j,

                new string[]
               {
                    item.MatriculaId.ToString(),
                    item.MatriculaStatus ? "Ativo" : "Inativo",
                    item.MatriculaAnoVigente.ToString(),
                    item.AlunoId.ToString(),
                    item.AlunoNome,
                    item.TipoMensalidadeDescricao,
                    item.MensalidadeId.ToString(),
                    item.MesalidadeDataVencimento.ToShortDateString(),
                    $"R$ {item.MensalidadeValor}",
                    item.MensalidadeParcela.ToString(),
                    item.MesalidadeMes.ToString(),
                    item.MesalidadeAno.ToString(),
                    item.MensalidadeTotalAtraso.ToString()
               });
                j++;
            }

            var nome = util.ExportarExcel(colunas, linhas, file);

            return Json(new { file.Name });
        }


        [HttpPost]
        [Route("Export/Excel/Alunos")]
        public async Task<IActionResult> ExportListaAlunosExcelAsync(bool? status)
        {
            string rootFolder = $"{ _hostingEnvironment.WebRootPath}\\xls";
            string fileName = $"{Guid.NewGuid()}.xlsx";
            var util = new Util(_hostingEnvironment);
            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));
            string[] colunas = new string[] {
                "Matricula Status",
                "Aluno ID",
                "Nome" ,
                "Email",
                "Telefone",
                "Celular",
                "Data Nascimento"
               };
            List<AlunoPorNomeQuery> list = (await _repositorioAluno.ObterTodosPorAsync(status)).ToList();
            Dictionary<int, string[]> linhas = new Dictionary<int, string[]>();
            int j = 0;
            foreach (var item in list)
            {
                linhas.Add(j,

                new string[]
               {

                    item.AlunoStatusMatricula ? "Ativo" : "Inativo",
                    item.AlunoId.ToString(),
                    item.AlunoNome,
                    item.AlunoEmail,
                    item.AlunoTelefone,
                    item.AlunoCelular,
                    item.AlunoDataToShortDate

               });
                j++;
            }

            var nome = util.ExportarExcel(colunas, linhas, file);

            return Json(new { file.Name });
        }
        [HttpGet]
        public ActionResult Download(string fileName)
        {
            string rootFolder = $"{ _hostingEnvironment.WebRootPath}/xls";

            byte[] fileByteArray = System.IO.File.ReadAllBytes(Path.Combine(rootFolder, fileName));
            System.IO.File.Delete(Path.Combine(rootFolder, fileName));
            return File(fileByteArray, "application/vnd.ms-excel", fileName);
        }


    }
}

