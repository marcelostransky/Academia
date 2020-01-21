using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada.Com_Matricula
{
    public class MatricularComando : Notifiable, IComando
    {
        public int Id { get; set; }
        public int IdAluno { get; set; }
        public string IdMatriculaGuid { get; set; }
        public DateTime DataContrato { get; set; }
        public decimal PercentualDesconto { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal ValorMatricula { get; set; }
        public decimal ValorContrato { get; set; }
        public int DiaVencimento { get; set; }
        public int MesInicioPagamento { get; set; }
        public int TotalParcelas { get; set; }
        public int Ano { get; set; }
        public DateTime DataIncialPagamento { get; set; }
        public Guid ChaveRegistro { get; set; }
        public List<MatriculaItemComando> Turmas { get; set; }
        public int TipoMatricula { get; set; }
        public MatricularComando()
        {
            Turmas = new List<MatriculaItemComando>();
        }
        public bool Valido()
        {
            AddNotifications(new ValidationContract()
               .IsTrue(DiaVencimento > 0, "Dia Vencimento", "Informe o dia de vencimento")
               .IsTrue(ValorContrato > 0, "Valor Contrato", "Informe o valor do contrato")
               .IsTrue(Ano > 0, "Ano", "Informe o Ano")
               .IsTrue(MesInicioPagamento > 0, "Mes Inicio Pagamento", "Informe o Mês início de pagamento")
               .IsTrue(TotalParcelas > 0, "TotalParcelas", "Informe o total de parcelas")
               .IsTrue((Id <= 0), "Id", "Id informado não é valido")
               .IsNotNullOrEmpty(DataIncialPagamento.ToShortDateString(), "Data Inicio Pagamento", "Informe a data inicial de pagamento")
               .IsNotNullOrEmpty(ChaveRegistro.ToString(), "Chave Registros", "Informe a chave de registros")
               .IsNotNullOrEmpty(IdMatriculaGuid, "Cod-Matricula", "Chave matricula não localizado")
               );
            return Valid;
        }
    }
}
