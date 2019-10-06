using AcademiaDanca.IO.Dominio.Contexto.Query.Acesso;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using AcademiaDanca.IO.Infra;
using AcademiaDanca.IO.Infra.Cache;
using AcademiaDanca.IO.Infra.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.App.Models
{
    public class RegrasAcessoModel
    {
        private readonly AcessoRepositorio _repositorio;
        private readonly IAcessoRepositorio _Irepositorio1;
        public IQueryable<PermissaoResultadoQuery> lista { get; set; }
        const string chave = "Academia.Danca.IO.Seguranca";


        public RegrasAcessoModel()
        {
            AcademiaContexto contexto = new AcademiaContexto();
            _repositorio = new AcessoRepositorio(contexto);


        }

        public IQueryable<PermissaoResultadoQuery> ObterListaPermissao(string perfil)
        {

            var cache = new CacheManager();
            var chavePerfil = $"{chave}.{perfil}";
            lista = cache.ObterDoCache<IQueryable<PermissaoResultadoQuery>>(chavePerfil);
            if (lista == null)
            {
                lista = _repositorio.ObterPermissaosAsync(perfil);
                cache.AdicionarAoCache(lista, chave, 14000);
            }
            return lista;
        }
        public bool PermitirAcesso(string perfil, string paginaId, string acao, string verbo)
        {
            try
            {
                var permitido = false;
                var listaPermissao = (ObterListaPermissao(perfil)).ToList();
                var pagina = (from s in listaPermissao
                              where s.PaginaId == paginaId
                              select s).FirstOrDefault();
                var nome = pagina.Ler.GetType().Name;

                var teste = nome;

                if (pagina.Ler.GetType().Name == verbo)
                    return pagina.Ler;
                else if (pagina.Criar.GetType().Name.Equals(verbo))
                    return pagina.Criar;
                else if (pagina.Editar.GetType().Name.Equals(verbo))
                    return pagina.Editar;
                else if (pagina.Excluir.GetType().Name.Equals(verbo))
                    return pagina.Excluir;
                else
                    return false;

                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
