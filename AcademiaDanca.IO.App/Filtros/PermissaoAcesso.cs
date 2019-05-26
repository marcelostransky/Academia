using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.App.Filtros
{
    public class PermissaoAcesso : Attribute, IActionFilter
    {
        //private string _area;
        private int _paginaId;
        private string _acao;
        private string _perfil;
        private string _verbo;
        public PermissaoAcesso(int pagina, string acao, string verbo)
        {
            _paginaId = pagina;
            _acao = acao;
            _verbo = verbo;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var perfil = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Papel").Value;
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {
     
            var teste = true;
        }
    }
}
