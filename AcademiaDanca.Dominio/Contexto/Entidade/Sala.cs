using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Entidade
{
    public class Sala : Notifiable

    {
        public int Id { get; private set; }
        public string DesSala { get; private set; }
        public Sala(int id)
        {
            Id = id;
        }
        public Sala(int id, string desSala)
        {
            Id = id;
            DesSala = desSala;
        }
    }
}
