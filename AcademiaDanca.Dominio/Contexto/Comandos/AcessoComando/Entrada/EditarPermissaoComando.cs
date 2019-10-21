using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.AcessoComando.Entrada
{
    public class EditarPermissaoComando : Notifiable, IComando
    {
        public int PerfilId { get; set; }
        public string Constante { get; set; }
        public bool Criar { get; set; }
        public bool Ler { get; set; }
        public bool Editar { get; set; }
        public bool Excluir { get; set; }
        public int PaginaId { get; set; }

        public bool Valido()
        {
            AddNotifications(new ValidationContract()
               .IsTrue(PaginaId > 0, "Codigo Pagina", "Informe a pagina")
               .IsTrue(PerfilId > 0, "Codigo Perfil", "Informe o perfil"));

            return Valid;
        }
       
    }
}
