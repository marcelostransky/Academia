using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AcademiaDanca.IO.App.Models;
using Microsoft.AspNetCore.Authorization;
using AcademiaDanca.IO.App.Filtros;

namespace AcademiaDanca.IO.App.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            //var fto = User.Claims.FirstOrDefault(x => x.Type == "Foto").Value;
            return View();
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
