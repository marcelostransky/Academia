using AcademiaDanca.IO.Infra.Cache;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.App.Helper
{
    public class Util
    {
        private readonly IHostingEnvironment _environment;
        private readonly CacheManager _cache;
        private readonly IConfiguration _config;
        public Util(IHostingEnvironment environment)
        {
            _environment = environment;
            _cache = new CacheManager();
        }
        public Util(IConfiguration config)
        {
            _cache = new CacheManager();
            _config = config;
        }

        public void RemoverItemCache(string perfil, string chave)
        {
            //var chaveMenu = _config.GetValue<string>("ChaveMenu");
            var chavePermissao = _config.GetValue<string>(chave);

            _cache.RemoverDoCache($"{chavePermissao}.{perfil}");
        }
        public string VerificarNomeArquivoCorreto(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

        public string ObterCaminhoArquivo(string nomeArquivo, string diretorio)
        {
            return _environment.WebRootPath + $"\\images\\avatars\\{diretorio}\\{nomeArquivo}";
        }

        public bool ExportarExcel(string[] colunas, Dictionary<int,string[]> data, FileInfo file)
        {
            var ok = true;
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Devedores");

                for(int col = 1; col <= colunas.Count(); col++)
                {
                    worksheet.Cells[1, col].Value = colunas[col-1];
                }

                for (int row = 2; row <= data.Count() + 1; row++)
                {
                    string[] lista = data[row - 2];
                    for (int col = 1; col <= lista.Count(); col++)
                    {
                        worksheet.Cells[row, col].Value = lista[col-1];
                    }

                }

                package.Save();

            }

            return ok;
        }
    }
}
