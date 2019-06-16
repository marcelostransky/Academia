using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query.Financeiro;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Repositorio
{
    public interface IMensalidadeRepositorio
    {
        Task<int> SalvarAsync(Mensalidade mensalidade);
        Task<int> EditarAsync(Mensalidade mensalidade);
        Task<int> DeletarAsync(int id);
        Task<IEnumerable<MensalidadesQueryResultado>> ObterMensalidadesAsync();
        Task<MensalidadesQueryResultado> ObterMensalidadePorAsync(int id);
     }
}
