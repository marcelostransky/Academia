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
    public class LoginRepositorio : ILoginRepositorio
    {
        private readonly AcademiaContexto _contexto;

        public LoginRepositorio(AcademiaContexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<AutenticarFuncionarioQueryResultado> Autenticar(Credencial credencial)
        {
            var usuarioAutenticado = await _contexto
                .Connection
                .QueryAsync<AutenticarFuncionarioQueryResultado>("sp_sel_usuario_autenticado", 
                                                        new { sp_login = credencial.Login, sp_senha = credencial.Senha }, 
                                                        commandType: CommandType.StoredProcedure);
            _contexto.Dispose();
            return usuarioAutenticado.FirstOrDefault();
        }

        public async Task<bool> CheckLoginAsync(string login)
        {
            var lista = await _contexto
                .Connection
                .QueryAsync<int>("SELECT Count(1) FROM academia.usuario where  login  = @nome;", new { nome = login }, commandType: CommandType.Text);
            _contexto.Dispose();
            return lista.FirstOrDefault() > 0 ? true : false;
        }

        public Task<int> SalvarAsync(Credencial funcionario)
        {
            throw new NotImplementedException();
        }
    }
}
