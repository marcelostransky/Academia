using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AcademiaDanca.IO.App.Filtros;
using AcademiaDanca.IO.App.Helper;
using AcademiaDanca.IO.App.Models;
using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaDanca.IO.App.Controllers
{
    [Authorize]

    public class LoginController : Controller
    {
        private readonly ILoginRepositorio _repositorio;
        private readonly IAcessoRepositorio _repositorioAcesso;
        public LoginController(ILoginRepositorio repositorio, IAcessoRepositorio repositorioAcesso)
        {
            _repositorio = repositorio;
            _repositorioAcesso = repositorioAcesso;
        }
        [AllowAnonymous]
        public IActionResult Autenticar()
        {
            ViewBag.Mensagem = "";
            return View();
        }
       
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Autenticar(string login, string senha)
        {

            var loginModel = new LoginModel(login, Criptografia.RetornaSenhaCriptografada(senha));
            var logado = await LoginUserAsync(loginModel.Login, loginModel.Senha);
            if (logado != null && logado.IdUsuario > 0)
            {
                var claims = new List<Claim>
            {
               new Claim(ClaimTypes.Sid,logado.IdUsuario.ToString()) ,
               new Claim(ClaimTypes.Name,logado.Nome),
               new Claim(ClaimTypes.Role,logado.Perfil),
               new Claim(ClaimTypes.Email,logado.Email),
               new Claim("Foto",logado.Foto),
               new Claim("Papel",logado.Perfil)
            };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);


                return Redirect("/");

            }
            ViewBag.Mensagem = "Login ou senha inválido";
            return View();
        }

        private async Task<AutenticarFuncionarioQueryResultado> LoginUserAsync(string login, string senha)
        {
            return await _repositorio.Autenticar(new Credencial(0, login, senha));
        }


        public async Task<IActionResult> LogoffAsync()
        {
            await HttpContext.SignOutAsync();
            await Task.Run(() => Redirect("/"));
            return Redirect("/");
        }
    }
}