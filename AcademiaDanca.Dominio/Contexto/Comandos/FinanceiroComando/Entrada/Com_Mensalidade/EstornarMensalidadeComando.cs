using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada.Com_Mensalidade
{
    public class EstornarMensalidadeComando : Notifiable, IComando
    {
        public int IdMensalidade { get; set; }
        public int IdUsuario { get; set; }
        public bool Valido()
        {
            AddNotifications(new ValidationContract()
                .IsTrue(IdMensalidade > 0, "Codigo Mensalidade", "Codigo mensalidade não localizado"));
            return Valid;
        }
    }
}
