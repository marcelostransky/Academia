using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.AcessoComando.Entrada
{
    public class EditarPaginaComando : Notifiable, IComando
    {
        public string DesPagina { get; set; }
        public string Chave { get; set; }
        public string DesPaginaAtual { get; set; }
        public string ChaveAtual { get; set; }
        public int Id { get; set; }
        public bool Valido()
        {
            AddNotifications(new ValidationContract()
                .IsTrue(Id > 0, "idTurma", "Informe o código.")
                .HasMinLen(DesPagina, 2, "Descricao", "Informe A Pagina")
                .HasMaxLen(DesPagina, 62, "Descricao", "Informe A Pagina")
                .HasMinLen(Chave, 2, "Chave", "Informe A Chave")
                .HasMaxLen(ChaveAtual, 20, "Chave", "Informe A Chave")
                .IsTrue(EdicaoLiberado(), "Chave/Descição", "Sistema não identificou dados para ser alterado"));
            return Valid;
        }
        public bool EdicaoLiberado()
        {
            return ((DesPagina != DesPaginaAtual) || (Chave != ChaveAtual));
        }

    }
}
