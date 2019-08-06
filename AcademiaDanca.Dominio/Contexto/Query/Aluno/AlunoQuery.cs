using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Query.Aluno
{
    public class AlunoQuery
    {
        public int AlunoId { get; set; }
        public string AlunoNome { get; set; }
        public string AlunoEmail { get; set; }
        public string AlunoCpf { get; set; }
        public string AlunoFoto { get; set; }
        public DateTime AlunodataNascimento { get; set; }
        public Guid AlunoGuid { get; set; }
        public string AlunoTelefone { get; set; }
        public string AlunoCelular { get; set; }
        public AlunoEnderecoQuery AlunoLogradouro { get; set; }
    }
}                                                                  
