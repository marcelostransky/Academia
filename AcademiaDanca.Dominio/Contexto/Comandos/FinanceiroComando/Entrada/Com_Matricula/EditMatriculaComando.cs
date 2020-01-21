using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada.Com_Matricula
{
    public class EditMatriculaComando : Notifiable, IComando
    {
        public string IdMatriculaGuid { get; set; }
        public bool StatusMatricula { get; set; }
        public List<MatriculaItemComando> Turmas { get; set; }

        public EditMatriculaComando()
        {
            Turmas = new List<MatriculaItemComando>();
        }
        public bool Valido()
        {
            AddNotifications(new ValidationContract()
              .IsNotNullOrEmpty(IdMatriculaGuid, "Codigo Matricula Não Informado", "Informe o dia de vencimento")
                 );
            return Valid;
        }
    }
}
