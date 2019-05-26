using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Vo
{
    public class Nome : Notifiable
    {
        public string Decricao { get; set; }
        public Nome(string decricao)
        {
            Decricao = decricao;
            AddNotifications(new ValidationContract()
               .Requires(  )
               .IsNotNullOrEmpty(Decricao,"Nome","Nome é um campo obrigatório.")
               .HasMinLen(Decricao, 3, "Nome", "O nome deve conter pelo menos 3 caracteres.")
               .HasMaxLen(Decricao, 300, "Nome", "O nome deve conter no máximo 300 caracteres.")
           );
        }
        public override string ToString()
        {
            return Decricao;
        }

    }
}
