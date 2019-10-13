using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.AcessoComando.Entrada
{
    public class AddPaginaComando : Notifiable, IComando
    {
        public string DesPagina { get; set; }
        public string Constante { get; set; }
        public int Id { get; set; }
        public bool Valido()
        {
            AddNotifications(new ValidationContract()
                .IsTrue(Id <= 0, "idTurma", "Informe a Turma")
                .HasMinLen(DesPagina,2, "Descricao", "Informe A Pagina")
                .HasMaxLen(DesPagina,62, "Descricao", "Informe A Pagina")
                .HasMinLen(Constante,2, "Constante", "Informe A Chave")
                .HasMaxLen(Constante,15, "Constante", "Informe A Chave"));
            return Valid;
        }
    }
}
