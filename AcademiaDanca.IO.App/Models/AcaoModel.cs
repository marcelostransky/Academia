using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using AcademiaDanca.IO.Infra.Cache;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.App.Models
{
    public class AcaoModel
    {
        private readonly IAcessoRepositorio _repositorio;
        private readonly IConfiguration _config;


        public AcaoModel(IAcessoRepositorio acessoRepositorio, IConfiguration config)
        {
            //AcademiaContexto contexto = new AcademiaContexto();
            _repositorio = acessoRepositorio;
            _config = config;
        }
        public async Task<IList<string>> ObterAcaoPerfilAsync(string perfil)
        {
            var chave = _config.GetValue<string>("ChaveAcao");
            var cache = new CacheManager();
            var chavePerfil = $"{chave}.{perfil}";
            var lista = cache.ObterDoCache<IList<string>>(chavePerfil);
            if (lista == null)
            {
                lista = new List<string>();
                var resultado = (await _repositorio.ObterAcaoPerfilAsync(perfil));
                foreach (var item in resultado)
                {
                    
                    if (!string.IsNullOrEmpty(item.Criar))
                        lista.Add($"{item.Criar}");
                    if (!string.IsNullOrEmpty(item.Ler))
                        lista.Add($"{item.Ler}");
                    if (!string.IsNullOrEmpty(item.Editar))
                        lista.Add($"{item.Editar}");
                    if (!string.IsNullOrEmpty(item.Deletar))
                        lista.Add($"{item.Deletar}");
                }
                cache.AdicionarAoCache(lista, chavePerfil, 14000);
            }
            return lista;
        }
    }
}
