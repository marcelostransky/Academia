using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Query.Aluno
{
    public class AlunoPorNomeQuery
    {
        public string AlunoEmail { get; set; }
        public int AlunoId { get; set; }
        public string AlunoUifId { get; set; }
        public string AlunoNome { get; set; }
        public string AlunoTelefone { get; set; }
        public string AlunoCelular { get; set; }
        public DateTime DataNascimento { get; set; }
        public string AlunoDataToShortDate { get { return DataNascimento.ToShortDateString(); } }
        public string AlunoFoto { get; set; }
        public bool AlunoStatusMatricula { get; set; }
    }
}
