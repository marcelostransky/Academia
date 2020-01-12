using AcademiaDanca.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query.Aluno;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Academia.Io.Tests.Fakes
{
    public class FakeAlunoRepositorio : IAlunoRepositorio
    {
        public Task<bool> CheckCpfAsync(string cpf)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<int> CheckFiliacaoAsync(Filiacao filiacao)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckTurmaAlunoAsync(TurmaAluno turmaAluno)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeletarTurmaAluno(TurmaAluno turmaAluno)
        {
            throw new NotImplementedException();
        }

        public Task<int> Editar(Aluno aluno)
        {
            throw new NotImplementedException();
        }

        public Task<int> EditarFotoAsync(Aluno aluno)
        {
            throw new NotImplementedException();
        }

        public Task<AlunoQuery> ObterAlunoCompletoAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Filiacao>> ObterFiliacaoAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Filiacao> ObterFiliacaoPorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AlunoSimplificadoQuery> ObterPorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AlunoSimplificadoQuery> ObterPorAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AlunoPorNomeQuery>> ObterPorTurmaAsync(int idTurma)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AddResponsavelQuery>> ObterTipoFiliacaoAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Aluno>> ObterTodosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AlunoPorNomeQuery>> ObterTodosPorAsync(string nome)
        {
            throw new NotImplementedException();
        }

        public Task<TotalTurmasQuery> ObterTotalTurmaAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> SalvarAsync(Aluno aluno)
        {
            throw new NotImplementedException();
        }

        public Task<int> SalvarFiliacaoAlunoAsync(int IdAluno, int IdResponsavel)
        {
            throw new NotImplementedException();
        }

        public Task<int> SalvarFiliacaoAsync(Filiacao filiacao)
        {
            throw new NotImplementedException();
        }

        public Task<int> SalvarTurmaAsync(TurmaAluno turmaAluno)
        {
            throw new NotImplementedException();
        }
    }
}
