using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Query.Financeiro
{
    public class MensalidadesQueryResultado
    {
        public int MensalidadeId { get; set; }
        public int AlunoId { get; set; }
        public int Parcela { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal Valor { get; set; }
        public decimal Desconto { get; set; }
        public string AlunoNome { get; set; }
        public bool Pago { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal Juros { get; set; }
        public int IdTipoMensalidade { get; set; }
        public string TipoMensalidade { get; set; }
        public bool Estorno { get; set; }
        public DateTime DataEstorno { get; set; }
    }
}
