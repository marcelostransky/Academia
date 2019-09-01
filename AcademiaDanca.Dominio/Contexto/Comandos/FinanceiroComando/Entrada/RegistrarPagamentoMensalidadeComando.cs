using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada
{
    public class RegistrarPagamentoMensalidadeComando : Notifiable, IComando
    {
        public int IdMensalidade { get; set; }
        public bool Pago { get; set; }
        public double Juros { get; set; }
        public bool Valido()
        {
            AddNotifications(new ValidationContract()
                .IsTrue(IdMensalidade <= 0, "Codigo Mensalidade", "Codigo mensalidade não localizado"));
            return Valid;
        }
    }
}
