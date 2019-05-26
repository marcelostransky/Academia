using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Repositorio
{
   public interface IFuncionarioRepositorio
    {
        Task<int> SalvarAsync(Funcionario funcionario, Credencial credencial);
        Task<int> EditarAsync(Funcionario funcionario);
        Task<int> EditarFotoAsync(Funcionario funcionario);
        Task<IEnumerable<FuncioanrioQueryPorNomeResultado>> ObterTodosAsync();
        Task<IEnumerable<FuncioanrioQueryPorNomeResultado>> ObterPorNomeAsync(string nome);
        Task<IEnumerable<FuncioanrioQueryPorNomeResultado>> ObterFuncionarioProfessorPorNomeAsync(string nome, int? id);
        Task<FuncioanrioQueryPorNomeResultado> ObterPorAsync(int id);
        Task<bool> CheckCpfAsync(string cpf);
        Task<bool> CheckEmailAsync(string email);
    }
}
