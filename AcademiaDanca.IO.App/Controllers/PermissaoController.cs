using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AcademiaDanca.IO.App.Models;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AcademiaDanca.IO.App.Controllers
{
    [Authorize]
    public class PermissaoController : Controller
    {
        private readonly IAcessoRepositorio _repositorioAcesso;
        private readonly IDashBoardRepositorio _repositorio;
        private readonly IConfiguration _config;
        public PermissaoController(IDashBoardRepositorio repositorio, IConfiguration config, IAcessoRepositorio acessoRepositorio)
        {
            _repositorio = repositorio;
            _repositorioAcesso = acessoRepositorio;
            _config = config;
        }
        public async Task<IActionResult> Menu()
        {
            try
            {
                var perfil = User.Claims.FirstOrDefault(x => x.Type == "Papel").Value;
                var menuModel = new MenuModel(_repositorioAcesso, _config);
                var resultado = (await menuModel.ObterConstanteMenuAsync(perfil)).ToArray();

                return Json(resultado);
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }

        }
        public async Task<IActionResult> Acoes()
        {
            try
            {
                var perfil = User.Claims.FirstOrDefault(x => x.Type == "Papel").Value;
                var menuModel = new AcaoModel(_repositorioAcesso, _config);
                var resultado = (await menuModel.ObterAcaoPerfilAsync(perfil)).ToArray();
                return Json(resultado);
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return Json(ex.Message);
            }

        }

    }
}