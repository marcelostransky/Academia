using AcademiaDanca.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query.Aluno;
using AcademiaDanca.IO.Dominio.Contexto.Query.Turma;
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
    public class TurmaRepositorio : ITurmaRepositorio
    {
        private readonly AcademiaContexto _contexto;

        public TurmaRepositorio(AcademiaContexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<bool> CheckTurmaAsync(Turma turma)
        {
            var existe = await _contexto
             .Connection
             .QueryAsync<int>("SELECT count(1) as total FROM academia.turma where id_turma_tipo = @idTurmaTipo and des_turma =@nome and cod_turma =@codTurma and ano =@ano ;",
                              new { idTurmaTipo = turma.TurmaTipo.Id, nome = turma.DesTurma, codTurma = turma.CodTurma, ano = turma.Ano },
                              commandType: CommandType.Text);

            return existe.FirstOrDefault() <= 0 ? false : true;
        }

        public async Task<IEnumerable<AlunoPorNomeQuery>> ObterAlunosPorAsync(int idTurma)
        {
            var parametros = new DynamicParameters();
            parametros.Add("sp_id_turma", idTurma);


            var listaAluno = await _contexto
                  .Connection
                  .QueryAsync<AlunoPorNomeQuery>(@"
                                      SELECT A.email as Email, A.id as Id, A.uif_id as UifId, 
                                      A.nome as Nome, A.telefone as Telefone, A.celular as Celular, 
                                      A.data_nascimento as DataNascimento, A.foto as Foto 
                                      FROM academia.turma as T
                                      Join academia.turma_aluno as TA on T.id = TA.id_turma
                                      Join academia.aluno as A On TA.id_aluno = A.id
                                      where T.id = @sp_id_turma;",
                  parametros,
                  commandType: System.Data.CommandType.Text);

            return listaAluno;
        }

        public Task<TurmaQueryResultado> ObterPorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TurmaQueryResultado>> ObterTodosAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TurmaQueryResultado>> ObterTodosPorAsync(int? idTurma = 0, int? idProfessor = 0, int? idTipoTurma = 0, int? ano = 0, int? idUsuario = 0)
        {
            var parametros = new DynamicParameters();
            parametros.Add("sp_id_turma", idTurma == 0 ? null : idTurma);
            parametros.Add("sp_id_turma_tipo", idTipoTurma == 0 ? null : idTipoTurma);
            parametros.Add("sp_id_professor", idProfessor == 0 ? null : idProfessor);
            parametros.Add("sp_ano", ano == 0 ? null : ano);
            parametros.Add("sp_id_usuario", idUsuario == 0 ? null : idUsuario);
            var lista = await _contexto.Connection.QueryAsync<TurmaQueryResultado>
                ("sp_sel_turma", param: parametros, commandType: System.Data.CommandType.StoredProcedure);
            return lista;

        }

        public Task<IEnumerable<TurmaQueryResultado>> ObterTodosPorAsync(int ano)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SalvarAsync(Turma turma)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("sp_cod_turma", turma.CodTurma);
                parametros.Add("sp_des_turma", turma.DesTurma);
                parametros.Add("sp_id_tipo_turma", turma.TurmaTipo.Id);
                parametros.Add("sp_id_professor", turma.Professor.Id);
                parametros.Add("sp_ano", turma.Ano);
                await _contexto
                    .Connection
                    .ExecuteAsync("sp_insert_turma",
                    parametros,
                    commandType: System.Data.CommandType.StoredProcedure);

                return parametros.Get<int>("sp_status");
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
