using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Query.Financeiro.Mensalidade
{
   public class MensalidadeVencidaQueryResultado
    {
        public int MatriculaId { get; set; }
        public bool MatriculaStatus { get; set; }
        public int MatriculaAnoVigente { get; set; }
        public int AlunoId { get; set; }
        public string AlunoNome { get; set; }
        public string TipoMensalidadeDescricao { get; set; }
        public int MensalidadeId { get; set; }
        public DateTime MesalidadeDataVencimento { get; set; }
        public decimal MensalidadeValor { get; set; }
        public int MensalidadeParcela { get; set; }
        public int MesalidadeMes { get; set; }
        public int MesalidadeAno { get; set; }
        public int MensalidadeTotalAtraso { get; set; }


    }
}
