using AcademiaDanca.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query.Aluno;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Repositorio
{
    public interface IAlunoRepositorio
    {
        Task<int> SalvarAsync(Aluno aluno);
        Task<int> SalvarResponsavelAsync(Filiacao filiacao);
        Task<IEnumerable<Aluno>> ObterTodosAsync();
        Task<IEnumerable<Filiacao>> ObterFiliacaoAsync();
        Task<IEnumerable<AddResponsavelQuery>> ObterTipoFiliacaoAsync();
        Task<Aluno> ObterPorAsync(int id);
        Task<Filiacao> ObterFiliacaoPorAsync(int id);
        Task<bool> CheckCpfAsync(string cpf);
        Task<bool> CheckEmailAsync(string email);
        Task<int> EditarFotoAsync(Aluno aluno);
        Task<int> SalvarTurmaAsync(TurmaAluno turmaAluno);
    }
}
