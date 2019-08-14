using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Query.Aluno
{
    public class AlunoPorNomeQuery
    {
        public string Email { get; set; }
        public int Id { get; set; }
        public string UifId { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public DateTime DataNascimento { get; set; }
        public string DataToShortDate { get { return DataNascimento.ToShortDateString(); } }
        public string Foto { get; set; }
    }
}
