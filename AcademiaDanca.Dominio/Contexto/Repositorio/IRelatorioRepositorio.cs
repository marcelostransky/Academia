using AcademiaDanca.IO.Dominio.Contexto.Query.Financeiro.Mensalidade;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Repositorio
{
    public interface IRelatorioRepositorio
    {
        Task<IEnumerable<MensalidadeVencidaQueryResultado>>ObterMensalidadesVencidasAsync(int? idMatricula, int? idMes, int? idAluno);
        Task<IEnumerable<EsperadoRealizadoQueryResultado>>ObterMensalidadesEsperadoRealizadoAsync(int? mes, int ano);
    }
}
