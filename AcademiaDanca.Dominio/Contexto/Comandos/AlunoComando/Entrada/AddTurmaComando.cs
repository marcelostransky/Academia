using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.AlunoComando.Entrada
{
    public class AddTurmaComando : Notifiable, IComando
    {
        public int IdTurma { get; set; }
        public int IdAluno { get; set; }
        public bool Valido()
        {
            AddNotifications(new ValidationContract()
                .IsTrue(IdTurma > 0, "idTurma", "Informe a Turma")
                .IsTrue(IdAluno > 0, "idAluno", "Informe o Aluno"));
            return Valid;
        }
    }
}
