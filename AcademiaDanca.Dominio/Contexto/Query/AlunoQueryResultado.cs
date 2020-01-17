using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Query
{
    public class AlunoQueryResultado
    {
        public int Id { get;  set; }
        public string AlunoGuid { get; set; }

        public string Nome { get;  set; }
        public string  Foto { get; set; }
        public DateTime DataNascimento { get;  set; }
        public string Endereco { get; private set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
    }
}
