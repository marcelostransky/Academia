using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query.Agenda;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Repositorio
{
    public interface IAgendaRepositorio
    {
        Task<int> SalvarAsync(Agenda agenda);
        Task<int> EditarAsync(Agenda agenda);
        Task<int> DeletarAsync(int id);
        Task<IEnumerable<DiaQueryResultado>> ObterTodosDiaSemanaAsync();
        Task<IEnumerable<AgendaQueryResultado>> ObterTodosAsync();
        Task<IEnumerable<AgendaQueryResultado>> ObterPorTurmaAsync(int id);
        Task<IEnumerable<AgendaQueryResultado>> ObterCalendarioPorDiaSemanaAsync(string diaSemana);
        Task<AgendaQueryResultado> ObterPorAsync(int id);
        Task<bool> CheckAgendamentoAsync(Agenda agenda);
        Task<IEnumerable<AgendaQueryResultado>> ObterPorAsync(int? id, int? idTurma, int? idSala, int? idDia, int? idProfessor,int? idTipoTurma, string hora);
    }
}
