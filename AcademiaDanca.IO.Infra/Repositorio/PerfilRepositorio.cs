using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Infra.Repositorio
{
    public class PerfilRepositorio : IPerfilRepositorio
    {
        private readonly AcademiaContexto _contexto;

        public PerfilRepositorio(AcademiaContexto contexto)
        {
            _contexto = contexto;
        }
        public Task<bool> CheckNomeAsync(string descPerfil)
        {
            throw new NotImplementedException();
        }

        public Task<PerfilQueryResultado> ObterPorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PerfilQueryResultado>> ObterTodosAsync()
        {
            var lista = await _contexto
               .Connection
               .QueryAsync<PerfilQueryResultado>("SELECT id, nome_papel as DescPerfil FROM academia.papel;", commandType: CommandType.Text);
            return lista;
        }

        public Task<int> SalvarAsync(Perfil perfil)
        {
            throw new NotImplementedException();
        }
    }
}
