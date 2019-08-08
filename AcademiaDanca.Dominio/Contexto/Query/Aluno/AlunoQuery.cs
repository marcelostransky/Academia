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
        public string AlunoGuid { get; set; }
        public string AlunoTelefone { get; set; }
        public string AlunoCelular { get; set; }
        public  AlunoEnderecoQuery AlunoLogradouro { get; set; }
        public  List<AlunoTurmaQuery> AlunoTurmas { get; set; }
        public  List<AlunoFiliacaoQuery> AlunoFiliacoes { get; set; }

        //public AlunoQuery()
        //{
        //    AlunoFiliacao = new List<AlunoFiliacaoQuery>();
        //    AlunoTurmas = new List<AlunoTurmaQuery>();
        //}
      
    }
}
