using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query.Sala;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Infra.Repositorio
{
    public class SalaRepositorio : ISalaRepositorio
    {
        private readonly AcademiaContexto _contexto;

        public SalaRepositorio(AcademiaContexto contexto)
        {
            _contexto = contexto;
        }
        public Task<bool> CheckSalaAsync(Sala sala)
        {
            throw new NotImplementedException();
        }

        public Task<SalaQueyResultado> ObterPorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SalaQueyResultado>> ObterTodosAsync()
        {
            var lista = await _contexto
               .Connection
               .QueryAsync<SalaQueyResultado>("SELECT id as Id, des_sala as DesSala FROM academia.sala  where status = 1;", commandType: CommandType.Text);
            return lista;
        }

        public Task<int> SalvarAsync(Sala sala)
        {
            throw new NotImplementedException();
        }
    }
}
