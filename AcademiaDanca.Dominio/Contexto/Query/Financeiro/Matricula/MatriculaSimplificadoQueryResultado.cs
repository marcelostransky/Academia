using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Query.Financeiro.Matricula
{
   public class MatriculaSimplificadoQueryResultado
    {
        public int IdMatricula { get; set; }
        public int IdAluno { get; set; }
        public bool Status { get; set; }
    }
}
