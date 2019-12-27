using AcademiaDanca.IO.Infra.Cache;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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
       
        public void RemoverItemCache(string perfil ,string chave)
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

      
    }
}
