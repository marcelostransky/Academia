using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Query.DashBoard
{
   public class QuantitativoAlunoAgendaMensalidadeQueryResultado
    {
        public int TotalAlunoMatriculado { get; set; }
        public int TotalMensalidadeAtraso { get; set; }
        public int TotalAgendamentoDia { get; set; }
        public int TotalMensalidadeHoje { get; set; }
    }
}
