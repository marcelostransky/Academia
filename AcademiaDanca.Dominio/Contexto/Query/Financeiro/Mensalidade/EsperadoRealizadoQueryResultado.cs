using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Query.Financeiro.Mensalidade
{
  public   class EsperadoRealizadoQueryResultado
    {
        public int Ano { get; set; }
        public int Mes { get; set; }
        public int DIa { get; set; }
        public int AlunoTotalEsperado { get; set; }
        public int AlunoTotalRealizado { get; set; }
        public int MensalidadeTotalEsperado { get; set; }
        public int MensalidadeTotalRealizado { get; set; }
        public decimal ValorTotalEsperado { get; set; }
        public decimal ValorTotalRealizado { get; set; }

    }
}
