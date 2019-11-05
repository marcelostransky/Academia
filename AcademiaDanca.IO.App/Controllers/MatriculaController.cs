using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademiaDanca.IO.App.Filtros;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaDanca.IO.App.Controllers
{
    [Authorize]
    [PermissaoAcesso(PaginaId = "MATR", Verbo = "Ler", TipoRetorno = "Html")]

    public class MatriculaController : Controller
    {
        private readonly IAcessoRepositorio _repositorioAcesso;
        private readonly IFinanceiroRepositorio _repositorio;
        private readonly IAlunoRepositorio _repositorioAluno;

        public MatriculaController(IFinanceiroRepositorio repositorio,
           IAlunoRepositorio repositorioAluno
           )
        {
            _repositorio = repositorio;
            _repositorioAluno = repositorioAluno;
           
        }

        public IActionResult Matricula(Guid alunoId)
        {
            return View();
        }
    }
}