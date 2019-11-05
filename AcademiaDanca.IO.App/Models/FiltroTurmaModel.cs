using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.App.Models
{
    public class FiltroTurmaModel
    {
        public int IdTurma { get; set; }
        public int IdTurmaTipo { get; set; }
        public int IdProfessor { get; set; }
        public int Ano { get; set; }
        public bool? Status { get; set; }
        public string TurmaDesc { get; set; }
    }
}
