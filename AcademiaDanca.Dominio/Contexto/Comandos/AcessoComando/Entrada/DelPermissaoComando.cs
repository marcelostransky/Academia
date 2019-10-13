using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.AcessoComando.Entrada
{
    public class DelPermissaoComando : Notifiable, IComando
    {
        public int PaginaId { get; set; }
        public int PerfilId { get; set; }
        public bool Valido()
        {
            AddNotifications(new ValidationContract()
                .IsTrue(PaginaId > 0, "Pagina Id", "Informe a pagina")
                .IsTrue(PerfilId > 0, "Perfil Id", "Informe o perfil"));

            return Valid;
        }
    }
}
