using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query;
using AcademiaDanca.IO.Dominio.Contexto.Query.Turma;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Infra.Repositorio
{
    public class TurmaTipoRepositorio : ITurmaTipoRepositorio
    {
        private readonly AcademiaContexto _contexto;

        public TurmaTipoRepositorio(AcademiaContexto contexto)
        {
            _contexto = contexto;
        }
        public Task<TurmaTipoQueryResultado> ObterPorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TurmaTipoQueryResultado>> ObterTodosAsync()
        {
            var lista = await _contexto
                .Connection
                .QueryAsync<TurmaTipoQueryResultado>("SELECT id As Id, des_turma_tipo as DesTurmaTipo FROM academia.turma_tipo  where status = 1;", commandType: CommandType.Text);
            return lista;
        }

        public Task<int> SalvarAsync(TurmaTipo turmaTipo)
        {
            throw new NotImplementedException();
        }
    }
}
