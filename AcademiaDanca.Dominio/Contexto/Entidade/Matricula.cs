using AcademiaDanca.Dominio.Contexto.Entidade;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Entidade
{
    public class Matricula
    {
        public int Id { get; set; }
        public Aluno Aluno { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataContrato { get; set; }
        public DateTime DataContratoTermino { get; set; }
        public float ValorContrato { get; set; }
        public float ValorMaricula { get; set; }

    }
}
