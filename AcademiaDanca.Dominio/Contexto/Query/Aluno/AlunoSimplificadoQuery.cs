using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Query.Aluno
{
 public   class AlunoSimplificadoQuery
    {
        public int AlunoId { get; set; }
        public string AlunoNome { get; set; }
        public string AlunoEmail { get; set; }
        public string AlunoCpf { get; set; }
        public DateTime AlunodataNascimento { get; set; }
        public string AlunoGuid { get; set; }
        public string AlunoTelefone { get; set; }
        public string AlunoCelular { get; set; }
    }
}
