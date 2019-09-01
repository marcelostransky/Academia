using AcademiaDanca.IO.Dominio.Contexto.Query.DashBoard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Repositorio
{
   public interface IDashBoardRepositorio
    {
        Task<IEnumerable<QuantitativoAlunoAgendaMensalidadeQueryResultado>> ObterQuantitativoAsync();
    }
}
