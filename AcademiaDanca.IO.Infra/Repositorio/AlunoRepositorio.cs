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

        public Task<int> SalvarResponsavelAsync(Filiacao filiacao)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SalvarTurmaAsync(TurmaAluno turmaAluno)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("sp_id_turma", turmaAluno.IdTurma);
                parametros.Add("sp_id_aluno", turmaAluno.IdAluno);

              var total =  await _contexto
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
    }
}
