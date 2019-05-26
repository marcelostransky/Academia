using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Vo
{
    public class Email : Notifiable
    {
        public Email(string endereco)
        {
            Endereco = endereco;

            AddNotifications(new ValidationContract()
                .Requires()
                .IsEmail(Endereco, "Email", "O E-mail é inválido")
            );
        }

        public string Endereco { get; private set; }

        public override string ToString()
        {
            return Endereco;
        }
    }
}
