using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada
{
    public class MatriculaItemComando : Notifiable, IComando
    {
        public int IdMatricula { get; set; }
        public int IdTurma { get; set; }
        public decimal Valor { get; set; }
        public bool Valido()
        {

            AddNotifications(new ValidationContract()
               .IsTrue(IdMatricula > 0, "Id Matricula", "Matricula não informada")
               .IsTrue(IdTurma > 0, "Id Turma", "Turma não informada")
               .IsTrue(Valor > 0, "Valor", "Valor não informado"));
            return Valid;
        }
    }
}
