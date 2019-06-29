using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Query.Turma
{
    public class TurmaQueryResultado
    {
        public int IdTurma { get; set; }
        public int Ano { get; set; }
        public int IdTipoTurma { get; set; }
        public int IdProfessor { get; set; }
        public string DesTurma { get; set; }
        public string DesTurmaTipo { get; set; }
        public string NomeProfessor { get; set; }
        public string CodTurma { get; set; }
        public string Foto { get; set; }
        public double Valor { get; set; }

    }
}
