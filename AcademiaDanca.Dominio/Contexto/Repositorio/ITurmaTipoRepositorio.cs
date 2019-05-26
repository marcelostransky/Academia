using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query.Turma;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Repositorio
{
    public interface ITurmaTipoRepositorio
    {
        Task<int> SalvarAsync(TurmaTipo turmaTipo);
        Task<IEnumerable<TurmaTipoQueryResultado>> ObterTodosAsync();
        Task<TurmaTipoQueryResultado> ObterPorAsync(int id);
    }
}
