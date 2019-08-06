using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Query.Aluno
{
    public class AlunoEnderecoQuery
    {
        public int LogradouroId { get; set; }
        public string LogradouroRua { get; set; }
        public string LogradouroNumenro { get; set; }
        public string LogradouroBairro { get; set; }
        public string LogradouroCep { get; set; }
        public string LogradouroComplemento { get; set; }
        public string LogradouroCidade { get; set; }
        public string LogradouroEstado { get; set; }

    }
}
