using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaDanca.IO.App.Controllers
{
    public class FinanceiroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Mensalidade(Guid id)
        {
            return View();
        }
        public IActionResult Matricular(Guid id)
        {
            return View();
        }
    }
}