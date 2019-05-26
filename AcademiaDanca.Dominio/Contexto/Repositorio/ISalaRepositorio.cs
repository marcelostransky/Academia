using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query.Sala;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Repositorio
{
    public interface ISalaRepositorio
    {
        Task<int> SalvarAsync(Sala sala);
        Task<IEnumerable<SalaQueyResultado>> ObterTodosAsync();
        Task<SalaQueyResultado> ObterPorAsync(int id);
        Task<bool> CheckSalaAsync(Sala sala);
    }
}
