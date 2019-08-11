using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Query.Aluno
{
   public class AlunoMensalidadeQuery
    {
        public int MensalidadeId { get; set; }
        public DateTime MensalidadeDataVencimento { get; set; }
        public decimal MensalidadeValor { get; set; }
        public decimal MensalidadeDesconto { get; set; }
        public bool MensalidadePago { get; set; }
        public DateTime MensalidadeDataPagamento { get; set; }
        public decimal MensalidadeJuros { get; set; }
        public int MensalidadeParcela { get; set; }
    }
}
