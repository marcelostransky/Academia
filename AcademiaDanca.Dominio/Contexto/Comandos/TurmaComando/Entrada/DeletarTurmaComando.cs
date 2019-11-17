using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.TurmaComando.Entrada
{
    public class DeletarTurmaComando : Notifiable, IComando
    {
        public int Id { get; set; }
        public bool Valido()
        {
            AddNotifications(new ValidationContract()
              .IsTrue(Id > 0, "ID", "Id informado não é válido")
          );
            return Valid;
        }
    }
}
