using AcademiaDanca.Dominio.Contexto.Entidade;
using FluentValidator;
using System;

namespace AcademiaDanca.IO.Dominio.Contexto.Entidade
{
    public class Agenda : Notifiable
    {
        public int Id { get; private set; }
        public string Hora { get; private set; }
        public Turma Turma { get; private set; }
        public DiaSemana DiaSemana { get; private set; }

        public Sala Sala { get; private set; }

        public Agenda(int id, 
            string hora, 
            Turma turma, 
            DiaSemana dia, 
            Sala sala)
        {
            Id = id;
            Hora = hora;
            Turma = turma;
            DiaSemana = dia;
            Sala = sala;

        }
    }
}
