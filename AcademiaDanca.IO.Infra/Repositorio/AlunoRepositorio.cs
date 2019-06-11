using AcademiaDanca.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
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

        public Task<Aluno> ObterPorAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<Aluno> ObterPorAsync(Guid uifId)
        {
            throw new NotImplementedException();
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
            parametros.Add("sp_email", aluno.Email!=null? aluno.Email.Endereco:null);
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
    }
}
