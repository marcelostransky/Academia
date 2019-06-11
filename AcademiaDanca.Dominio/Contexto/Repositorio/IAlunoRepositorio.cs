using AcademiaDanca.Dominio.Contexto.Entidade;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Repositorio
{
    public interface IAlunoRepositorio
    {
        Task<int> SalvarAsync(Aluno aluno);
        Task<IEnumerable<Aluno>> ObterTodosAsync();
        Task<Aluno> ObterPorAsync(int id);
        Task<bool> CheckCpfAsync(string cpf);
        Task<bool> CheckEmailAsync(string email);
        Task<int> EditarFotoAsync(Aluno aluno);
    }
}
