using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query.Acesso;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Dapper;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Infra.Repositorio
{
    public class AcessoRepositorio : IAcessoRepositorio
    {
        private readonly AcademiaContexto _contexto;

        public AcessoRepositorio(AcademiaContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> CheckPaginaAsync(string desPagina)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_descricao", desPagina);
                var total = (await _contexto
                      .Connection
                      .QueryAsync<int>("SELECT count(1)  FROM academia.pagina where  des_pagina   = @sp_descricao;",
                      parametros,
                      commandType: System.Data.CommandType.Text)).FirstOrDefault();

                return total > 0;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> CheckPerfilAsync(string desPerfil)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_descricao", desPerfil);
                var total = (await _contexto
                      .Connection
                      .QueryAsync<int>("SELECT count(1)  FROM academia.papel where  nome_papel = @sp_descricao;",
                      parametros,
                      commandType: System.Data.CommandType.Text)).FirstOrDefault();

                return total > 0;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task<int> DeletarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> EditarAsync(Pagina pagina)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PaginaResultadoQuery>> ObterPaginasAsync()
        {

            try
            {
                return await _contexto
                        .Connection
                        .QueryAsync<PaginaResultadoQuery>(" SELECT id as Id, des_pagina as DesPagina FROM academia.pagina;",

                        commandType: System.Data.CommandType.Text);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<IEnumerable<PerfilResultadoQuery>> ObterPerfisAsync()
        {

            try
            {
                return await _contexto
                        .Connection
                        .QueryAsync<PerfilResultadoQuery>(" SELECT id as Id, nome_papel as DesPerfil, status as Status FROM academia.papel;",

                        commandType: System.Data.CommandType.Text);
            }
            catch (Exception ex)
            {

                var msg = ex.Message;
                throw;
            }
        }
        public async Task<int> SalvarAsync(Pagina pagina)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("sp_nome", pagina.DesPagina);

                await _contexto
                    .Connection
                    .ExecuteAsync("sp_insert_pagina",
                    parametros,
                    commandType: System.Data.CommandType.StoredProcedure);

                return parametros.Get<int>("sp_id");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> SalvarPerfilAsync(Perfil perfil)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("sp_nome", perfil.DesPerfil);

                await _contexto
                    .Connection
                    .ExecuteAsync("sp_insert_perfil",
                    parametros,
                    commandType: System.Data.CommandType.StoredProcedure);

                return parametros.Get<int>("sp_id");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
