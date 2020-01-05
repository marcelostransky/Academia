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
        public string IdMatriculaGuid { get; set; }
        public int IdTurma { get; set; }
        public int ValorDesconto { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorCalculado { get; set; }
        public bool AplicarDesconto { get; set; }
        public bool Valido()
        {

            AddNotifications(new ValidationContract()
               .IsNotNullOrEmpty(IdMatriculaGuid.ToString(), "Id Matricula", "Matricula não informada")
               .IsTrue(IdTurma > 0, "Id Turma", "Turma não informada")
               .IsTrue(Valor > 0, "Valor", "Valor não informado"));
            return Valid;
        }
    }
}
