using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.App.Areas.Admin.Models
{
    public class DadosAlunoModel
    {
        public string CodAluno { get; set; }
        public string Nome { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Endereco { get; set; }
        public string Filiacao1 { get; set; }
        public string Filiacao2 { get; set; }
        public string DataNascimento { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
    }
}
