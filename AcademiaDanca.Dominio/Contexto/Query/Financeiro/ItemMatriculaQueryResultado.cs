using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Query.Financeiro
{
    public class ItemMatriculaQueryResultado
    {
        public int IdMatricula { get; set; }
        public string IdMatriculaGuid { get; set; }
        public string CodTurma { get; set; }
        public int IdTurma { get; set; }
        public decimal Valor { get; set; }
        public bool Desconto { get; set; }
        public int ValorDesconto { get; set; }
        public decimal ValorCalculado { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorTotalDesconto { get; set; }
    }
}
