using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query.Agenda;
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
    public class AgendaRepositorio : IAgendaRepositorio
    {
        private readonly AcademiaContexto _contexto;

        public AgendaRepositorio(AcademiaContexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<bool> CheckAgendamentoAsync(Agenda agenda)
        {

            var lista = await _contexto
             .Connection
             .QueryAsync<int>("SELECT Count(1) FROM academia.agenda where hora =@hora and id_sala =@idSala and id_dia_semana = @idDia;", new { hora = agenda.Hora, idSala = agenda.Sala.Id, idDia = agenda.DiaSemana.Id }, commandType: CommandType.Text);

            return lista.FirstOrDefault() > 0 ? true : false;

        }

        public Task<int> EditarAsync(Agenda agenda)
        {
            throw new NotImplementedException();
        }

        public Task<AgendaQueryResultado> ObterPorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AgendaQueryResultado>> ObterPorTurmaAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<AgendaQueryResultado>> ObterPorAsync(int? id, int? idTurma, int? idSala, int? idDia,int? idProfessor,int? idTurmaTipo,string hora)
        {
            var parametros = new DynamicParameters();
            parametros.Add("sp_id", id);
            parametros.Add("sp_id_turma", idTurma);
            parametros.Add("sp_id_sala", idSala);
            parametros.Add("sp_id_dia", idDia);
            parametros.Add("sp_id_professor", idProfessor);
            parametros.Add("sp_id_tipo_turma", idTurmaTipo);
            parametros.Add("sp_hora", hora);
            var lista = await _contexto
                .Connection
                .QueryAsync<AgendaQueryResultado>("sp_sel_agenda", parametros, commandType: CommandType.StoredProcedure);
            return lista;
        }
        public Task<IEnumerable<AgendaQueryResultado>> ObterTodosAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DiaQueryResultado>> ObterTodosDiaSemanaAsync()
        {
            var lista = await _contexto
          .Connection
          .QueryAsync<DiaQueryResultado>("SELECT id as Id,des_dia_semana as DiaSemana,sgl_dia_semana as DiaSemanaSigla FROM academia.dia_semana;", commandType: CommandType.Text);
            return lista;
        }

        public async Task<int> SalvarAsync(Agenda agenda)
        {
            var parametros = new DynamicParameters();
            parametros.Add("sp_id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("sp_id_turma", agenda.Turma.Id);
            parametros.Add("sp_id_sala", agenda.Sala.Id);
            parametros.Add("sp_id_dia", agenda.DiaSemana.Id);
            parametros.Add("sp_hora", agenda.Hora);

            await _contexto
                .Connection
                .ExecuteAsync("sp_insert_agenda",
                parametros,
                commandType: System.Data.CommandType.StoredProcedure);

            return parametros.Get<int>("sp_id");
        }

        public async Task<int> DeletarAsync(int id)
        {
            var parametros = new DynamicParameters();
          
            parametros.Add("sp_id", id);
           

            var deletado = await _contexto
                .Connection
                .ExecuteAsync("delete from academia.agenda where id = @sp_id",
                parametros,
                commandType: System.Data.CommandType.Text);

            return deletado;
        }

        public async Task<IEnumerable<AgendaQueryResultado>> ObterCalendarioPorDiaSemanaAsync(string diaSemana)
        {
            var parametros = new DynamicParameters();
           
            parametros.Add("sp_dia_semana", diaSemana);
            
           
            var lista = await _contexto
                .Connection
                .QueryAsync<AgendaQueryResultado>(@"SELECT 
                                                    A.id as IdAgenda,
                                                    T.id as IdTurma,
                                                    T.cod_turma as CodTurma,
                                                    T.des_turma as DesTurma,
                                                    S.id as IdSala,
                                                    S.des_sala as DesSala,
                                                    D.id AS IdDia,
                                                    D.sgl_dia_semana as SiglaDia,
                                                    D.des_dia_semana as DiaSemna,
                                                    A.hora as Hora,
                                                    F.id as IdProfessor,
                                                    F.nome as Professor,
                                                    TT.id as idTipoTurma,
                                                    TT.des_turma_tipo AS TipoTurma,
                                                    Count(TA.id_aluno) As TotalAluno
                                                    FROM academia.agenda as A
                                                    Join academia.turma as T on A.id_turma = T.id
                                                    left join academia.turma_aluno as TA on T.id = TA.id_turma
                                                    Join academia.sala as S on A.id_sala = S.id
                                                    Join academia.dia_semana as D on A.id_dia_semana = D.id
                                                    Join academia.turma_professor as TU On T.id = TU.id_turma
                                                    Join academia.funcionario as F On TU.id_funcionario = F.id
                                                    Join academia.turma_tipo as TT On  T.id_turma_tipo = TT.id
                                                    where T.status = 1 and  D.des_dia_semana = @sp_dia_semana
                                                    Group By
                                                    A.id,
                                                    T.id,
                                                    T.cod_turma,
                                                    T.des_turma,
                                                    S.id,
                                                    S.des_sala,
                                                    D.id,
                                                    D.sgl_dia_semana,
                                                    D.des_dia_semana,
                                                    A.hora,
                                                    F.id,
                                                    F.nome,
                                                    TT.id,
                                                    TT.des_turma_tipo    ", parametros, commandType: CommandType.Text);
            return lista;
        }
    }
}
