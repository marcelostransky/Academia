using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AcademiaDanca.IO.App.Areas.Admin.Models;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.DadosExternos.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.DadosExternos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.XlsIO;

namespace AcademiaDanca.IO.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DadosExternosController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private DadosExternosManipulador _manipulador;
        public DadosExternosController(IHostingEnvironment host, DadosExternosManipulador manipulador)
        {
            _hostingEnvironment = host;
            _manipulador = manipulador;
        }
        public IActionResult Index()
        {
            DadosExternosComando comando = new DadosExternosComando();
            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;
            application.DefaultVersion = ExcelVersion.Excel2013;
            string basePath = _hostingEnvironment.WebRootPath + @"\Data\Aluno.xlsx";
            FileStream sampleFile = new FileStream(basePath, FileMode.Open);
            IWorkbook workbook = application.Workbooks.Open(sampleFile);
            IWorksheet worksheet = workbook.Worksheets[0];
           
            for (var i = 0; i < worksheet.Rows.Count(); i++)
            {
                if (i > 0)
                {
                    comando.lista.Add(new DadosExternosComando
                    {
                        CodAluno = worksheet.Rows[i].Columns[0].Text,
                        Nome = worksheet.Rows[i].Columns[1].Text,
                        DataNascimento = worksheet.Rows[i].Columns[7].Value,
                        Tel1 = worksheet.Rows[i].Columns[3].Text,
                        Tel2 = worksheet.Rows[i].Columns[4].Text,
                        Filiacao1 = worksheet.Rows[i].Columns[5].Text,
                        Filiacao2 = worksheet.Rows[i].Columns[6].Text,
                        Endereco = worksheet.Rows[i].Columns[2].Text,
                        Cep = worksheet.Rows[i].Columns[8].Text,
                        Bairro = worksheet.Rows[i].Columns[9].Text,
                        Cidade = worksheet.Rows[i].Columns[10].Text
                    });
                }
                //for (var j = 0; j < worksheet.Rows[i].Columns.Count(); j++)
                //{
                //    var valor = worksheet.Rows[i].Columns[j].Text;
                //}
                //var nome = worksheet.Rows[i, 0].Text;
            }
            _manipulador.ManipuladorAsync(comando);
            return View();
        }

        [Route("/Integracao/Aluno/Add")]
        public async Task<IActionResult> IntegrarAluno()
        {
            ExcelEngine excelEngine = new ExcelEngine();


            return await Task.Run(() => Json(new { a = 1 }));

        }
    }
}