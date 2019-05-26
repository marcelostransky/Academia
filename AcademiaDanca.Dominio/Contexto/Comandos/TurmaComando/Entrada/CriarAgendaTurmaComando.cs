using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.TurmaComando.Entrada
{
    public class CriarAgendaTurmaComando : Notifiable, IComando
    {
        public int Id { get; set; }
        public string Hora { get; set; }
        public int IdTurma { get; set; }
        public int IdDiaSemana { get; set; }
        public int IdSala { get; set; }
        public bool Valido()
        {
            AddNotifications(new ValidationContract()
               .IsTrue(Id > 0, "Id", "Id informado não é válido")
               .IsNullOrEmpty(Hora, "Hora", "Hora do agendamento não informada")

           );
            return Valid;
        }
    }
}
