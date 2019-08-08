using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Query.Aluno
{
  public  class AlunoFiliacaoQuery
    {
        public int FiliacaoId { get; set; }
        public string FiliacaoNome { get; set; }
        public string FiliacaoTelefone { get; set; }
        public string FiliacaoDocumento { get; set; }
        public string FiliacaoEmail { get; set; }
    }
}
