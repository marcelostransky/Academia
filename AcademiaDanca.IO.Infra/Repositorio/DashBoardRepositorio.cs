using AcademiaDanca.IO.Dominio.Contexto.Query.Agenda;
using AcademiaDanca.IO.Dominio.Contexto.Query.DashBoard;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Infra.Repositorio
{
    public class DashBoardRepositorio : IDashBoardRepositorio
    {
        private readonly AcademiaContexto _contexto;

        public DashBoardRepositorio(AcademiaContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<QuantitativoAlunoAgendaMensalidadeQueryResultado>> ObterQuantitativoAsync()
        {
            //var parametros = new DynamicParameters();
            //parametros.Add("sp_id", id);
            //parametros.Add("sp_id_turma", idTurma);
            //parametros.Add("sp_id_sala", idSala);
            //parametros.Add("sp_id_dia", idDia);
            //parametros.Add("sp_id_professor", idProfessor);
            //parametros.Add("sp_id_tipo_turma", idTurmaTipo);
            //parametros.Add("sp_hora", hora);
            var lista = await _contexto
                .Connection
                .QueryAsync<QuantitativoAlunoAgendaMensalidadeQueryResultado>("sp_sel_dash_board",  commandType: CommandType.StoredProcedure);
            _contexto.Dispose();
            return lista;
        }
    }
}
