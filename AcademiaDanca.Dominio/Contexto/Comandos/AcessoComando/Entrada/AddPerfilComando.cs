using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.AcessoComando.Entrada
{
    public class AddPerfilComando : Notifiable, IComando
    {
        public string DesPerfil { get; set; }
        public int Id { get; set; }
        public bool Valido()
        {
            AddNotifications(new ValidationContract()
                .IsTrue(Id > 0, "idTurma", "Informe a Turma")
                .IsNullOrEmpty(DesPerfil, "Descricao", "Informe A Pagina"));
            return Valid;
        }
      
    }
}
