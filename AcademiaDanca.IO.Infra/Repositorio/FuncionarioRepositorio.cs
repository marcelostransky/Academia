using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query;
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
    public class FuncionarioRepositorio : IFuncionarioRepositorio
    {
        private readonly AcademiaContexto _contexto;

        public FuncionarioRepositorio(AcademiaContexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<bool> CheckCpfAsync(string cpf)
        {
            var lista = await _contexto
              .Connection
              .QueryAsync<int>("SELECT count(1) FROM academia.funcionario where cpf = @cpf;", new { cpf = cpf }, commandType: CommandType.Text);

            return lista.FirstOrDefault() > 0 ? true : false;
        }

        public async Task<bool> CheckEmailAsync(string email)
        {
            var lista = await _contexto
             .Connection
             .QueryAsync<int>("SELECT count(1) FROM academia.funcionario where email = @email;", new { email }, commandType: CommandType.Text);

            return lista.FirstOrDefault() > 0 ? true : false;
        }

        public async Task<int> EditarAsync(Funcionario funcionario)
        {
            try
            {
                var parametros = new DynamicParameters();

                parametros.Add("sp_id", funcionario.Id);
                parametros.Add("sp_nome", funcionario.Nome.ToString());
                parametros.Add("sp_email", funcionario.Email.ToString());
                parametros.Add("sp_cpf", funcionario.Cpf.ToString());
                parametros.Add("sp_id_perfil", funcionario.IdPerfil);
                parametros.Add("sp_data_nascimento", funcionario.DataNascimento);
                var editado = await _contexto
                    .Connection
                    .ExecuteAsync("sp_edit_funcionario",
                    parametros,
                    commandType: System.Data.CommandType.StoredProcedure);

                return editado;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<int> EditarFotoAsync(Funcionario funcionario)
        {
            var parametros = new DynamicParameters();

            parametros.Add("sp_id", funcionario.Id);
            parametros.Add("sp_foto", funcionario.Foto);
            var editado = await _contexto
                  .Connection
                  .ExecuteAsync("sp_edit_foto_funcionario",
                  parametros,
                  commandType: System.Data.CommandType.StoredProcedure);

            return editado;
        }

        public async Task<int> EditarAcessoAsync(int funcionarioId, string senha)
        {
            try
            {
                var parametros = new DynamicParameters();

                parametros.Add("sp_id", funcionarioId);
                parametros.Add("sp_senha", senha);
                var editado = await _contexto
                      .Connection
                      .ExecuteAsync(@"update  academia.usuario as F 
                                      Inner Join academia.usuario_funcionario_papel as FP On F.id = FP.id_usuario
                                      set senha = @sp_senha where FP.id_funcionario = @sp_id; ",
                      parametros,
                      commandType: System.Data.CommandType.Text);

                return editado;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
        public async Task<FuncioanrioQueryPorNomeResultado> ObterPorAsync(int id)
        {
            var parametros = new DynamicParameters();
            parametros.Add("sp_nome", null, DbType.String);
            parametros.Add("sp_id", id, DbType.Int32);
            var lista = await _contexto
           .Connection
           .QueryAsync<FuncioanrioQueryPorNomeResultado>("sp_sel_funcionario", parametros, commandType: CommandType.StoredProcedure);
            return lista.FirstOrDefault();
        }

        public async Task<IEnumerable<FuncioanrioQueryPorNomeResultado>> ObterPorNomeAsync(string nome)
        {
            var lista = await _contexto
             .Connection
             .QueryAsync<FuncioanrioQueryPorNomeResultado>("sp_sel_funcionario", new { sp_nome = nome, sp_id = 0 }, commandType: CommandType.StoredProcedure);
            return lista;
        }
        public async Task<IEnumerable<FuncioanrioQueryPorNomeResultado>> ObterFuncionarioProfessorPorNomeAsync(string nome, int? id)
        {
            var lista = await _contexto
             .Connection
             .QueryAsync<FuncioanrioQueryPorNomeResultado>("sp_sel_professor", new { sp_nome = nome, sp_id = id }, commandType: CommandType.StoredProcedure);
            return lista;
        }
        public Task<IEnumerable<FuncioanrioQueryPorNomeResultado>> ObterTodosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> SalvarAsync(Funcionario funcionario)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SalvarAsync(Funcionario funcionario, Credencial credencial)
        {
            var parametros = new DynamicParameters();

            parametros.Add("sp_status", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("sp_nome", funcionario.Nome.ToString());
            parametros.Add("sp_email", funcionario.Email.ToString());
            parametros.Add("sp_cpf", funcionario.Cpf.ToString());
            parametros.Add("sp_foto", funcionario.Foto);
            parametros.Add("sp_login", credencial.Login);
            parametros.Add("sp_senha", credencial.Senha);
            parametros.Add("sp_id_perfil", funcionario.IdPerfil);
            parametros.Add("sp_data_nascimento", funcionario.DataNascimento);
            await _contexto
                .Connection
                .ExecuteAsync("sp_insert_funcionario",
                parametros,
                commandType: System.Data.CommandType.StoredProcedure);

            return parametros.Get<int>("sp_status");
        }

        public async Task<bool> CheckPerfilProfessorTurmaAtivaAsync(int id)
        {
            var permitido = await _contexto
             .Connection
             .QueryAsync<int>(@"SELECT count(PO.id_turma) as Total
                                FROM academia.funcionario as F
                                Join academia.usuario_funcionario_papel as UFP ON F.id = UFP.id_funcionario
                                Join academia.papel as P On UFP.id_papel = P.id
                                Join academia.turma_professor as PO on F.id = PO.id_funcionario
                                Where  ufp.id_papel = 4 and F.id = @FuncionarioId;", new { FuncionarioId = id }, commandType: CommandType.Text);

            return permitido.FirstOrDefault() > 0;
        }
    }
}
