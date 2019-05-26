using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.TurmaComando.Entrada
{
    public class DeletarAgendaTurmaComando : Notifiable, IComando
    {
        public int IdAgenda { get; set; }
        public bool Valido()
        {
            AddNotifications(new ValidationContract()
              .IsTrue(IdAgenda > 0, "ID", "Id informado não é válido")
          );
            return Valid;
        }
    }
}
