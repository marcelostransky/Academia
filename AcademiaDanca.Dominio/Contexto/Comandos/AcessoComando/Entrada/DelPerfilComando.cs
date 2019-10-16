using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.AcessoComando.Entrada
{
    public class DelPerfilComando : Notifiable, IComando
    {
        public int Id { get; set; }

        public bool Valido()
        {
            AddNotifications(new ValidationContract()
               .IsTrue(Id > 0, "Perfil Id", "Código do perfil não informado."));

            return Valid;
        }
    }
}
