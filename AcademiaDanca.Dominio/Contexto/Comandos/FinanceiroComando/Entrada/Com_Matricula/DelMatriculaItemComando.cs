using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada.Com_Matricula
{
    public class DelMatriculaItemComando : Notifiable, IComando
    {
        public string IdMatricula { get; set; }
        public int IdTurma { get; set; }
        public bool Valido()
        {
            AddNotifications(new ValidationContract()
                    .IsNotNullOrEmpty(IdMatricula, "Id", "Id informado não é valido")
                    .IsTrue((IdTurma <= 0), "Id", "Id informado não é valido")
                  );
            return Valid;
        }
    }
}
