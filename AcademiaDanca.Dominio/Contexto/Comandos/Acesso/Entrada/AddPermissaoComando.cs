using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.Acesso.Entrada
{
    public class AddPermissaoComando : Notifiable, IComando
    {
        public int PaginaId { get; set; }
        public int PerfilId { get; set; }
        public string DesPagina { get; set; }
        public string DesPerfil { get; set; }
        public bool Criar { get; set; }
        public bool Ler { get; set; }
        public bool Editar { get; set; }
        public bool Excluir { get; set; }
        public bool Valido()
        {
            AddNotifications(new ValidationContract()
               .IsTrue(PaginaId > 0, "Codigo Pagina", "Informe a pagina")
               .IsTrue(PerfilId > 0, "Codigo Perfil", "Informe o perfil"));
               
            return Valid;
        }
    }
}
