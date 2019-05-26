using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Entidade
{
    public class DiaSemana : Notifiable
    {
        public int Id { get; set; }
        public string DesDiaSemana { get; private set; }

        public string SiglaDiaSemana { get; private set; }
        public DiaSemana(int id)
        {
            Id = id;
        }
        public DiaSemana(int id, string dia, string sigla)
        {
            Id = id;
            DesDiaSemana = dia;
            SiglaDiaSemana = sigla;

        }
    }
}
