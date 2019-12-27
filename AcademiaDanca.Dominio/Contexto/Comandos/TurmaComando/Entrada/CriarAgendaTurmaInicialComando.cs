using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.TurmaComando.Entrada
{
    public class CriarAgendaTurmaInicialComando : Notifiable
    {
        public string Hora { get; set; }
        public string Dia { get; set; }
        public int Sala { get; set; }
        public bool Valido()
        {
            AddNotifications(new ValidationContract()
               
               .IsNullOrEmpty(Dia, "Dia", "Dia do agendamento não informada")

           );
            return Valid;
        }

    }
}
