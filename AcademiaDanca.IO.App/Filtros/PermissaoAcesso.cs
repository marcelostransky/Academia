using AcademiaDanca.IO.App.Models;
using AcademiaDanca.IO.Dominio.Contexto.Query.Acesso;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Microsoft.AspNetCore.Mvc;
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
        public string PaginaId { get; set; }
        public string Acao { get; set; }
        public string Perfil { get; set; }
        public string Verbo { get; set; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Perfil = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Papel").Value;
            var model = new RegrasAcessoModel();
            var regras = model.PermitirAcesso(Perfil, PaginaId, null, Verbo);
            if (!regras)
                context.Result = new RedirectResult($"/Home/NaoAutorizado/");
        }

        //public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        //{
           
        //   await next();
        //}
    }
}
