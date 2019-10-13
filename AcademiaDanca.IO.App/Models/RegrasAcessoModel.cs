using AcademiaDanca.IO.Dominio.Contexto.Query.Acesso;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using AcademiaDanca.IO.Infra;
using AcademiaDanca.IO.Infra.Cache;
using AcademiaDanca.IO.Infra.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AcademiaDanca.IO.App.Models
{
    public class RegrasAcessoModel
    {
        private readonly IAcessoRepositorio _repositorio;


        const string chave = "Academia.Danca.IO.Seguranca";


        public RegrasAcessoModel(IAcessoRepositorio acessoRepositorio)
        {
            //AcademiaContexto contexto = new AcademiaContexto();
            _repositorio = acessoRepositorio;
                //--new AcessoRepositorio(contexto);


        }

        public List<PermissaoResultadoQuery> ObterListaPermissao(string perfil)
        {


            var cache = new CacheManager();
            var chavePerfil = $"{chave}.{perfil}";
            var lista = cache.ObterDoCache<List<PermissaoResultadoQuery>>(chavePerfil);
            if (lista == null)
            {
                lista = _repositorio.ObterPermissaosAsync(perfil);
                cache.AdicionarAoCache(lista, chavePerfil, 14000);
            }
            return lista;
        }
        public bool PermitirAcesso(string perfil, string paginaId, string acao, string verbo, string tipoRetorno)
        {
            try
            {

                var pagina = ObterPagina(perfil, paginaId);
                if (pagina == null)
                    return false;
                if (nameof(pagina.Ler) == verbo)
                    return pagina.Ler;
                else if (nameof(pagina.Criar) == verbo)
                    return pagina.Criar;
                else if (nameof(pagina.Editar) == (verbo))
                    return pagina.Editar;
                else if (nameof(pagina.Excluir) == verbo)
                    return pagina.Excluir;
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private PermissaoResultadoQuery ObterPagina(string perfil, string pagina)
        {
            var listaPermissao = (ObterListaPermissao(perfil)).ToList();
            var resultado = (from s in listaPermissao
                             where s.PaginaId == pagina
                             select s).FirstOrDefault();
            return resultado;
        }
    }
}
