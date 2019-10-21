using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using AcademiaDanca.IO.Infra.Cache;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.App.Models
{
    public class MenuModel
    {
        private readonly IAcessoRepositorio _repositorio;
        private readonly IConfiguration _config;




        public MenuModel(IAcessoRepositorio acessoRepositorio, IConfiguration config)
        {
            //AcademiaContexto contexto = new AcademiaContexto();
            _repositorio = acessoRepositorio;
            _config = config;
        }
        //--new AcessoRepositorio(contexto);



        public async Task<IEnumerable<string>> ObterConstanteMenuAsync(string perfil)
        {

            var chave = _config.GetValue<string>("ChaveMenu");
            var cache = new CacheManager();
            var chavePerfil = $"{chave}.{perfil}";
            var lista = cache.ObterDoCache<IEnumerable<string>>(chavePerfil);
            if (lista == null)
            {
                lista = await _repositorio.ObterConstanteMenuAsync(perfil);
                cache.AdicionarAoCache(lista, chavePerfil, 14000);
            }
            return lista;
        }

    }
}
