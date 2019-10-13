using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.AcessoComando.Entrada
{
    public class EditarPerfilComando : Notifiable, IComando
    {
        public string DesPerfil { get; set; }
        public string DesPerfilAtual { get; set; }
        public int Id { get; set; }
        public bool Valido()
        {
            AddNotifications(new ValidationContract()
                .IsTrue(Id > 0, "IdPerfil", "Informe o código do perfil.")
                .HasMinLen(DesPerfil, 2, "Descricao", "Informe A Pagina")
                .HasMaxLen(DesPerfilAtual, 62, "Descricao", "Informe A Pagina")
                .IsTrue(EdicaoLiberada(), "Chave/Descição", "Sistema não identificou dados para ser alterado"));
            return Valid;
        }
        public bool EdicaoLiberada()
        {
            return (DesPerfil != DesPerfilAtual);
        }
     
    }
}
