using AcademiaDanca.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query.Aluno;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AcademiaDanca.IO.Infra.Repositorio
{
    public class AlunoRepositorio : IAlunoRepositorio
    {

        private readonly AcademiaContexto _contexto;

        public AlunoRepositorio(AcademiaContexto contexto)
        {
            _contexto = contexto;
        }

        public Task<bool> CheckCpfAsync(string cpf)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CheckFiliacaoAsync(Filiacao filiacao)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_email", filiacao.Email.Endereco);
                var id = (await _contexto
                      .Connection
                      .QueryAsync<int>("SELECT id FROM academia.filiacao where  email = @sp_email;",
                      parametros,
                      commandType: System.Data.CommandType.Text)).FirstOrDefault();

                return id;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<bool> CheckTurmaAlunoAsync(TurmaAluno turmaAluno)
        {
            var parametros = new DynamicParameters();
            parametros.Add("sp_id_aluno", turmaAluno.IdAluno);
            parametros.Add("sp_id_turma", turmaAluno.IdTurma);
            var existe = (await _contexto
                  .Connection
                  .QueryAsync<int>("SELECT Count(1) FROM academia.turma_aluno where id_aluno = @sp_id_aluno and id_turma = @sp_id_turma;",
                  parametros,
                  commandType: System.Data.CommandType.Text)).FirstOrDefault();

            return existe > 0;
        }

        public async Task<int> DeletarTurmaAluno(TurmaAluno turmaAluno)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id_turma", turmaAluno.IdTurma);
                parametros.Add("sp_id_aluno", turmaAluno.IdAluno);

                var total = await _contexto
                      .Connection
                      .ExecuteAsync("sp_delete_turma_aluno",
                      parametros,
                      commandType: System.Data.CommandType.StoredProcedure);

                return total;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> EditarFotoAsync(Aluno aluno)
        {
            var parametros = new DynamicParameters();

            parametros.Add("sp_id", aluno.Id);
            parametros.Add("sp_foto", aluno.Foto);
            var editado = await _contexto
                  .Connection
                  .ExecuteAsync("sp_edit_foto_aluno",
                  parametros,
                  commandType: System.Data.CommandType.StoredProcedure);

            return editado;
        }

        public Task<int> MatricularAssync(Matricula matricula)
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

        public Task<Aluno> ObterPorAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<Aluno> ObterPorAsync(Guid uifId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AddResponsavelQuery>> ObterTipoFiliacaoAsync()
        {
            var query = @"SELECT id as Id, des_tipo_filiacao as Nome FROM academia.tipo_filiacao;";
            return await _contexto.Connection.QueryAsync<AddResponsavelQuery>(query, commandType: CommandType.Text);
        }

        public Task<IEnumerable<Aluno>> ObterTodosAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TotalTurmasQuery> ObterTotalTurmaAsync(Guid id)
        {
            var query = @"SELECT count(ta.id_turma) as Total, 
                         CAST(sum(t.valor) as decimal(7,2)) Valor FROM academia.turma as t
                         join academia.turma_aluno as ta on t.id = ta.id_turma
                         join academia.aluno as a on ta.id_aluno = a.id
                         where a.uif_id = @id";
            var parametros = new DynamicParameters();
            parametros.Add("id", id);

            return (await _contexto.Connection.QueryAsync<TotalTurmasQuery>(query, parametros, commandType: CommandType.Text)).FirstOrDefault();
        }

        public async Task<int> SalvarAsync(Aluno aluno)
        {
            var parametros = new DynamicParameters();
            parametros.Add("sp_id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("sp_nome", aluno.Nome);
            parametros.Add("sp_email", aluno.Email != null ? aluno.Email.Endereco : null);
            parametros.Add("sp_cpf", aluno.Cpf != null ? aluno.Cpf.Numero : null);
            parametros.Add("sp_foto", aluno.Foto);
            parametros.Add("sp_data_nascimento", aluno.DataNascimento);
            parametros.Add("sp_uif_id", aluno.UifId);
            parametros.Add("sp_telefone", aluno.Telefone);
            parametros.Add("sp_celular", aluno.Celular);
            await _contexto
                .Connection
                .ExecuteAsync("sp_insert_aluno",
                parametros,
                commandType: System.Data.CommandType.StoredProcedure);

            return parametros.Get<int>("sp_id");
        }



        public async Task<int> SalvarFiliacaoAsync(Filiacao filiacao)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("sp_nome", filiacao.Nome);
                parametros.Add("sp_telefone", filiacao.Telefone);
                parametros.Add("sp_email", filiacao.Email != null ? filiacao.Email.Endereco : null);
                parametros.Add("sp_documento", filiacao.Documento != null ? filiacao.Documento.Numero : null);
                parametros.Add("sp_id_tipo_filiacao", filiacao.IdTipoFiliacao);
                await _contexto
                    .Connection
                    .ExecuteAsync("sp_insert_filiacao",
                    parametros,
                    commandType: System.Data.CommandType.StoredProcedure);
                return parametros.Get<int>("sp_id");
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<int> SalvarFiliacaoAlunoAsync(int IdAluno, int IdFiliacao)
        {
            try
            {
                var parametros = new DynamicParameters();

                parametros.Add("sp_id_aluno", IdAluno);
                parametros.Add("sp_id_filiacao", IdFiliacao);

                var inserido = await _contexto
                    .Connection
                    .ExecuteAsync("sp_insert_filiacao_aluno",
                    parametros,
                    commandType: System.Data.CommandType.StoredProcedure);
                return inserido;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<int> SalvarTurmaAsync(TurmaAluno turmaAluno)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("sp_id_turma", turmaAluno.IdTurma);
                parametros.Add("sp_id_aluno", turmaAluno.IdAluno);

                var total = await _contexto
                      .Connection
                      .ExecuteAsync("sp_insert_turma_aluno",
                      parametros,
                      commandType: System.Data.CommandType.StoredProcedure);

                return total;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<IEnumerable<AlunoPorNomeQuery>> ObterTodosPorAsync(string nome)
        {
            var parametros = new DynamicParameters();
            parametros.Add("sp_nome", nome);
          
            var lista = await _contexto.Connection.QueryAsync<AlunoPorNomeQuery>
                ("sp_sel_aluno_por_nome", param: parametros, commandType: System.Data.CommandType.StoredProcedure);
            return lista;

        }
    }
}
