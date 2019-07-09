using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada
{
    public class MatricularComando : Notifiable, IComando
    {
        public int Id { get; set; }
        public int IdAluno { get; set; }
        public DateTime DataContrato { get; set; }
        public decimal PercentualDesconto { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal ValorMaricula { get; set; }
        public decimal ValorContrato { get; set; }
        public int DiaVencimento { get; set; }
        public int TotalParcelas { get; set; }
        public int Ano { get; set; }
        public DateTime DataIncialPagamento { get; set; }
        public Guid ChaveRegistro { get; set; }

        public bool Valido()
        {
            AddNotifications(new ValidationContract()
               .IsTrue(DiaVencimento <= 0, "Dia Vencimento", "Informe o dia de vencimento")
               .IsTrue(ValorContrato <= 0, "Valor Contrato", "Informe o valor do contrato")
               .IsTrue(Ano <= 0, "Ano", "Informe o Ano")
               .IsTrue(TotalParcelas <= 0, "TotalParcelas", "Informe o total de parcelas")
               .IsTrue((Id > 0), "Id", "Id informado não é valido")
               .IsNullOrEmpty(DataIncialPagamento.ToShortDateString(),"Data Inicio Pagamento","Informe a data inicial de pagamento" )
               .IsNullOrEmpty(ChaveRegistro.ToString(),"Chave Registros","Informe a chave de registros")
               );
            return Valid;
        }
    }
}
