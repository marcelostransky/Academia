using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.AcessoComando.Entrada
{
    public class DelPaginaComando :Notifiable, IComando
    {
        public int PaginaId { get; set; }

        public bool Valido()
        {
            AddNotifications(new ValidationContract()
                .IsTrue(PaginaId > 0, "Pagina Id", "Código da página não informada."));

            return Valid;
        }
    }
}
