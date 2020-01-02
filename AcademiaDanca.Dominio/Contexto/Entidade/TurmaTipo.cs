using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Entidade
{
    public class TurmaTipo
    {
        public int Id { get; private set; }
        public string DesTurmaTipo { get; private set; }
        public decimal ValorHora { get; private set; }

        public TurmaTipo(int id)
        {
            Id = id;
          
        }
        public TurmaTipo(int id, string desTurmaTipo, decimal valorHora)
        {
            Id = id;
            DesTurmaTipo = desTurmaTipo;
            ValorHora = valorHora;
        }
    }
}
