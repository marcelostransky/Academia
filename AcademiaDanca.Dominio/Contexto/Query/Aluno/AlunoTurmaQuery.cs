using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Query.Aluno
{
    public class AlunoTurmaQuery
    {
        public int TurmaId { get; set; }
        public string TurmaCodigo { get; set; }
        public int TurmaAno { get; set; }
        public string TurmaDescricao { get; set; }
        public decimal TurmaValor { get; set; }

    }
}
