using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Query.Turma
{
    public class TurmaQueryResultado
    {
        public int IdTurma { get; set; }
        public int Ano { get; set; }
        public int AnoAtual { get; set; }
        public int IdTipoTurma { get; set; }
        public int IdTipoTurmaAtual { get; set; }
        public int IdProfessor { get; set; }
        public string DesTurma { get; set; }
        public string DesTurmaAtual { get; set; }
        public string DesTurmaTipo { get; set; }
        public string NomeProfessor { get; set; }
        public string CodTurma { get; set; }
        public string CodTurmaAtual { get; set; }
        public string Foto { get; set; }
        public decimal Valor { get; set; }
        public bool Status { get; set; }

    }
}
