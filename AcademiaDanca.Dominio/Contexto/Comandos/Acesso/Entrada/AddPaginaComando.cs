using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.Acesso.Entrada
{
    public class AddPaginaComando : Notifiable, IComando
    {
        public string DesPagina { get; set; }
        public int Id { get; set; }
        public bool Valido()
        {
            AddNotifications(new ValidationContract()
                .IsTrue(Id > 0, "idTurma", "Informe a Turma")
                .IsNullOrEmpty(DesPagina, "Descricao", "Informe A Pagina"));
            return Valid;
        }
    }
}
