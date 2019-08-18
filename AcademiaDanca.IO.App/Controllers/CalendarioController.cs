using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademiaDanca.IO.Compartilhado.Util;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Leanwork.CodePack.DataTables;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaDanca.IO.App.Controllers
{
    public class CalendarioController : Controller
    {
        private readonly IAgendaRepositorio _repositorio;
        public CalendarioController(IAgendaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("Calendario/Agenda/DiaSemana")]
        public async Task<IActionResult> ObterAgendamentos(string data, jQueryDataTableRequestModel request)
        {
            try
            {
                DateTime dataConsulta = DateTime.Now;
                if (!string.IsNullOrEmpty(data))
                {
                    dataConsulta = Convert.ToDateTime(data);
                }
               
                var diaSemana = DateTimeExtension.GetDayOfWeek(dataConsulta, new System.Globalization.CultureInfo("pt-Br"));
                var lista = (await _repositorio.ObterCalendarioPorDiaSemanaAsync(diaSemana)).AsQueryable();

                if (request.sSearch != null && request.sSearch.Length > 0)
                {
                    lista = lista.Where(x => x.DesTurma.ToUpper().Contains(request.sSearch.ToUpper()));
                }

                var model = (from r in lista
                             select new
                             {   data= dataConsulta.ToShortDateString(),
                                 r.IdTurma,
                                 r.Professor,
                                 r.DesTurma,
                                 r.CodTurma,
                                 r.TipoTurma,
                                 r.SiglaDia,
                                 r.Hora,
                                 r.TotalAluno


                             }).DataTableResponse(request);

                return Ok(model);

            }
            catch (System.Exception ex)
            {
                throw;
            }
            //var draw = requestformdata["draw"];
            //dynamic response = new
            //{
            //    Data = lista.ToList(),
            //    Draw = "1",
            //    RecordsFiltered = lista.Count(),
            //    RecordsTotal = lista.Count()
            //};

        }

    }
}