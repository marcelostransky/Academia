using AcademiaDanca.Dominio.Contexto.Entidade;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Repositorio
{
    public interface IEnderecoRepositorio
    {
        Task<int> SalvarAsync(Endereco endereco, int idAluno);
        Task<IEnumerable<Endereco>> ObterTodosAsync();
        Task<Endereco> ObterPorAsync(int id);
        Task<int> EditarFotoAsync(Endereco endereco);
    }
}
