using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Query.Aluno
{
    public class AlunoMatriculaQuery
    {
        public int MatriculaId { get; set; }
        public string MatriculaGuid { get; set; }
        public decimal MatriculaValorContrato { get; set; }
        public int MatriculaPercentualDesconto { get; set; }
        public decimal MatriculaValorDesconto { get; set; }
        public int MatriculaDiaVencimento { get; set; }
        public DateTime MatriculaDataInicialPagamento { get; set; }
        public decimal MatriculaValorMatricula { get; set; }
        public int MatriculaTotalParcelas { get; set; }
        public int MatriculaAno { get; set; }
        public string MatriculaStatus { get; set; }
    }
}
