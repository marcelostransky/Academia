using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Entidade
{
    public class MatriculaItemTemp
    {
        public string IdMatricula { get; set; }
        public int IdTurma { get; set; }
        public decimal Valor { get; set; }
        public bool Desconto { get; set; }
        public int ValorDesconto { get; set; }
        public decimal ValorCalculado { get; set; }
        public MatriculaItemTemp(string idMatricula, int idTurma, decimal valor,
            bool desconto, int valorDesconto, decimal valorCalculado)
        {
            IdMatricula = idMatricula;
            IdTurma = idTurma;
            Valor = valor;
            Desconto = desconto;
            ValorDesconto = valorDesconto;
            ValorCalculado = valorCalculado;
        }
    }
}
