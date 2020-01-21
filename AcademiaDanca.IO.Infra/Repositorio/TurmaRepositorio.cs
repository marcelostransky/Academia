using AcademiaDanca.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query.Agenda;
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

        public async Task<bool> CheckAgendamentoTurmaAsync(int id)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id", id);
                var existe = await _contexto
            .Connection
            .QueryAsync<int>(@"SELECT Count(1) FROM academia.turma_aluno where
                                id_turma = @sp_id ;",
                             parametros,
                             commandType: CommandType.Text);

                return existe.FirstOrDefault() > 0 ? true : false;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _contexto.Dispose();
            }
        }

        public async Task<bool> CheckAlunoTurmaAsync(int id)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id", id);
                var existe = await _contexto
            .Connection
            .QueryAsync<int>(@"SELECT Count(1) FROM academia.turma_aluno where
                                id_turma = @sp_id ;",
                             parametros,
                             commandType: CommandType.Text);

                return existe.FirstOrDefault() > 0 ? true : false;
            }
            catch (Exception)
            {

                throw;
            }
            finally { _contexto.Dispose(); }

        }

        public async Task<TurmaQuantitativoQueryResultado> CheckQuantitativoTurmaAsync(int id)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id", id);
                var quantitativos = await _contexto
            .Connection
            .QueryAsync<TurmaQuantitativoQueryResultado>(@"sp_valida_delete_turma;",
                             parametros,
                             commandType: CommandType.StoredProcedure);

                return quantitativos.FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
            finally { _contexto.Dispose(); }
        }

        public async Task<bool> CheckTurmaAsync(Turma turma)
        {
            try
            {

                var existe = await _contexto
             .Connection
             .QueryAsync<int>(@"SELECT count(1) as total FROM academia.turma 
                                where id_turma_tipo = @idTurmaTipo and 
                                des_turma =@nome and 
                                cod_turma =@codTurma  
                                ;",
                              new { idTurmaTipo = turma.TurmaTipo.Id, nome = turma.DesTurma, codTurma = turma.CodTurma },
                              commandType: CommandType.Text);

                return existe.FirstOrDefault() <= 0 ? false : true;
            }
            catch (Exception)
            {

                throw;
            }
            finally { _contexto.Dispose(); }

        }

        public async Task<int> DeletarAsync(int id)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id", id);
                var retorno = await _contexto
                    .Connection
                    .ExecuteAsync(@"sp_delete_turma",
                    parametros,
                    commandType: System.Data.CommandType.StoredProcedure);

                return retorno;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { _contexto.Dispose(); }
        }
        public async Task<int> DeletarAlunoAsync(int idAluno, int idTurma)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id_aluno", idAluno);
                parametros.Add("sp_id_turma", idTurma);
                var retorno = await _contexto
                    .Connection
                    .ExecuteAsync(@"update  `academia`.`turma_aluno` set status = 0
                                    WHERE id_aluno =@sp_id_aluno  and id_turma =@sp_id_turma ",
                    parametros,
                    commandType: System.Data.CommandType.Text);

                return retorno;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { _contexto.Dispose(); }
        }


        public async Task<int> EditarAsync(Turma turma)
        {
            try
            {
                var parametros = new DynamicParameters();

                parametros.Add("sp_id", turma.Id);
                parametros.Add("sp_cod_turma", turma.CodTurma);
                parametros.Add("sp_des_turma", turma.DesTurma);
                parametros.Add("sp_id_tipo_turma", turma.TurmaTipo.Id);
                parametros.Add("sp_id_professor", turma.Professor.Id);

                parametros.Add("sp_status", turma.Status);
                var retorno = await _contexto
                    .Connection
                    .ExecuteAsync(@"sp_edit_turma",
                    parametros,
                    commandType: System.Data.CommandType.StoredProcedure);

                return retorno;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { _contexto.Dispose(); }
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
            _contexto.Dispose();
            return listaAluno;
        }

        public async Task<IEnumerable<DiaQueryResultado>> ObterDiaSemana()
        {

            var listaDia = await _contexto
                  .Connection
                  .QueryAsync<DiaQueryResultado>(@"
                                      SELECT id as Id,des_dia_semana as DiaSemana,sgl_dia_semana as DiaSemanaSigla FROM academia.dia_semana ;",

                  commandType: System.Data.CommandType.Text);
            _contexto.Dispose();
            return listaDia;
        }



        public Task<TurmaQueryResultado> ObterPorAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<TurmaQueryResultado>> ObterTodosAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<TurmaQueryResultado>> ObterTodosPorAsync(int? idTurma = 0, int? idProfessor = 0, int? idTipoTurma = 0, bool? status = null, int? idUsuario = 0)
        {
            var parametros = new DynamicParameters();
            parametros.Add("sp_id_turma", idTurma == 0 ? null : idTurma);
            parametros.Add("sp_id_turma_tipo", idTipoTurma == 0 ? null : idTipoTurma);
            parametros.Add("sp_id_professor", idProfessor == 0 ? null : idProfessor);
            parametros.Add("sp_id_usuario", idUsuario == 0 ? null : idUsuario);
            parametros.Add("sp_status", status);
            var lista = await _contexto.Connection.QueryAsync<TurmaQueryResultado>
                ("sp_sel_turma", param: parametros, commandType: System.Data.CommandType.StoredProcedure);
            _contexto.Dispose();
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
                parametros.Add("sp_id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("sp_cod_turma", turma.CodTurma);
                parametros.Add("sp_des_turma", turma.DesTurma);
                parametros.Add("sp_id_tipo_turma", turma.TurmaTipo.Id);
                parametros.Add("sp_id_professor", turma.Professor.Id);

                await _contexto
                    .Connection
                    .ExecuteAsync("sp_insert_turma",
                    parametros,
                    commandType: System.Data.CommandType.StoredProcedure);

                return parametros.Get<int>("sp_id");
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { _contexto.Dispose(); }

        }
    }
}
