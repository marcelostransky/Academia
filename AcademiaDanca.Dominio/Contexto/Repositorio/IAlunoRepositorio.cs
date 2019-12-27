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
        Task<int> DeletarTurmaAluno(TurmaAluno turmaAluno);
        Task<int> SalvarTurmaAsync(TurmaAluno turmaAluno);
        Task<int> SalvarFiliacaoAsync(Filiacao filiacao);
        Task<int> SalvarFiliacaoAlunoAsync(int IdAluno, int IdResponsavel);
        Task<IEnumerable<Aluno>> ObterTodosAsync();
        Task<IEnumerable<Filiacao>> ObterFiliacaoAsync();
        Task<IEnumerable<AddResponsavelQuery>> ObterTipoFiliacaoAsync();
        Task<AlunoSimplificadoQuery> ObterPorAsync(int id);
        Task<AlunoSimplificadoQuery> ObterPorAsync(Guid id);
        Task<Filiacao> ObterFiliacaoPorAsync(int id);
        Task<bool> CheckCpfAsync(string cpf);
        Task<bool> CheckEmailAsync(string email);
        Task<int> EditarFotoAsync(Aluno aluno);
        Task<int> Editar(Aluno aluno);
       
        Task<bool> CheckTurmaAlunoAsync(TurmaAluno turmaAluno);
        Task<int> CheckFiliacaoAsync(Filiacao filiacao);
        Task<TotalTurmasQuery> ObterTotalTurmaAsync(Guid id);
        Task<IEnumerable<AlunoPorNomeQuery>> ObterTodosPorAsync(string nome);
        Task<IEnumerable<AlunoPorNomeQuery>> ObterPorTurmaAsync(int idTurma);
        Task<AlunoQuery> ObterAlunoCompletoAsync(Guid id);
    }
}
