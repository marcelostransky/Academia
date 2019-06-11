using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.App.Helper
{
    public class Util
    {
        private readonly IHostingEnvironment _environment;
        public Util(IHostingEnvironment environment)
        {
            _environment = environment;
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
    }
}
