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

        public async Task<bool> CheckFuncionarioPerfilAsync(int id)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id", id);
               
                var total = (await _contexto
                      .Connection
                      .QueryAsync<int>("SELECT count(1)  FROM academia.usuario_funcionario_papel where  id_papel =@sp_id ;",
                      parametros,
                      commandType: System.Data.CommandType.Text)).FirstOrDefault();

                return total > 0;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> CheckPaginaAsync(string desPagina, string chave)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_descricao", desPagina);
                parametros.Add("sp_constante", chave);
                var total = (await _contexto
                      .Connection
                      .QueryAsync<int>("SELECT count(1)  FROM academia.pagina where  des_pagina = @sp_descricao and constante = @sp_constante ;",
                      parametros,
                      commandType: System.Data.CommandType.Text)).FirstOrDefault();

                return total > 0;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task<int> CheckPaginaPermissaoAsync(int id)
        {
            throw new NotImplementedException();
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

        public async Task<bool> CheckPermissaoAsync(int? paginaId, int? perfilId)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_pagina_id", paginaId == 0 ? null : paginaId);
                parametros.Add("sp_papel_id", perfilId == 0 ? null : perfilId);
                var total = (await _contexto
                      .Connection
                      .QueryAsync<int>("SELECT count(1) FROM academia.pagina_papel where id_pagina = ifnull(@sp_pagina_id,id_pagina) and id_papel=ifnull(@sp_papel_id,id_papel);",
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

        public async Task<bool> DeletarPaginaAsync(int perfilId)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_perfil_id", perfilId);

                var total = (await _contexto
                      .Connection
                      .ExecuteAsync("sp_delete_perfil",
                      parametros,
                      commandType: System.Data.CommandType.StoredProcedure));

                return total > 0;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task<bool> DeletarPerfilAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeletarPermissaoAsync(int perfilId, int paginaId)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_pagina_id", paginaId);
                parametros.Add("sp_papel_id", perfilId);
                var total = (await _contexto
                      .Connection
                      .ExecuteAsync("sp_delete_permissao",
                      parametros,
                      commandType: System.Data.CommandType.StoredProcedure));

                return total > 0;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> EditaPaginaAsync(Pagina pagina)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_des_pagina", pagina.DesPagina);
                parametros.Add("sp_constante", pagina.Constante);
                parametros.Add("sp_id", pagina.Id);
                var processado = (await _contexto
                      .Connection
                      .ExecuteAsync("sp_edit_pagina",
                      parametros,
                      commandType: System.Data.CommandType.StoredProcedure));

                return processado;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task<int> EditarAsync(Pagina pagina)
        {
            throw new NotImplementedException();
        }
        public async Task<int> EditarPerfilAsync(Perfil perfil)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_des_perfil", perfil.DesPerfil);
                parametros.Add("sp_id", perfil.Id);
                var processado = (await _contexto
                      .Connection
                      .ExecuteAsync("sp_edit_perfil",
                      parametros,
                      commandType: System.Data.CommandType.StoredProcedure));

                return processado;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<IEnumerable<PaginaResultadoQuery>> ObterPaginasAsync()
        {

            try
            {
                return await _contexto
                        .Connection
                        .QueryAsync<PaginaResultadoQuery>(" SELECT id as Id, des_pagina as DesPagina, constante as Constante FROM academia.pagina;",

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

        public async Task<IEnumerable<PermissaoResultadoQuery>> ObterPermissaosAsync(string paginaId, string perfilId)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id_pagina", paginaId);
                parametros.Add("sp_id_perfil", perfilId);
                return await _contexto
                        .Connection
                        .QueryAsync<PermissaoResultadoQuery>(@"
                        SELECT 
                       
                        pg.constante as PaginaId,
                        pg.des_pagina as DesPagina,
                        p.id as PapelId,
                        p.nome_papel as DesPapel,
                        pp.Criar as Criar,
	                    pp.deletar as Excluir,
                        pp.atualizar as Editar,
                        pp.ler as Ler
                        FROM academia.pagina as pg
                        LEFT JOIN academia.pagina_papel as pp  on pg.id = pp.id_pagina
                        LEFT JOIN academia.papel as p on pp.id_papel = p.id
                        Where
                        pg.constante = ifnull(@sp_id_pagina, pg.constante)
                        And
                        p.nome_papel = ifnull(@sp_id_perfil, p.nome_papel);", param: parametros,

                        commandType: System.Data.CommandType.Text);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<PermissaoResultadoQuery> ObterPermissaosAsync(string perfil)
        {
            try
            {

                return _contexto
                        .Connection
                        .Query<PermissaoResultadoQuery>(@"
                        SELECT 
                       
                        pg.constante as PaginaId,
                        pg.des_pagina as DesPagina,
                        p.id as PapelId,
                        p.nome_papel as DesPapel,
                        pp.Criar as Criar,
	                    pp.deletar as Excluir,
                        pp.atualizar as Editar,
                        pp.ler as Ler
                        FROM academia.pagina as pg
                         JOIN academia.pagina_papel as pp  on pg.id = pp.id_pagina
                         JOIN academia.papel as p on pp.id_papel = p.id
                        Where     p.nome_papel = @perfil
                      ;", param: new { perfil }, commandType: System.Data.CommandType.Text).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<int> SalvarAsync(Pagina pagina)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("sp_nome", pagina.DesPagina);
                parametros.Add("sp_constante", pagina.Constante);

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

        public async Task<int> SalvarPermissaoAsync(Permissao permissao)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_pagina_id", permissao.PaginaId);
                parametros.Add("sp_papel_id", permissao.PerfilId);
                parametros.Add("sp_criar", permissao.Criar);
                parametros.Add("sp_atualizar", permissao.Editar);
                parametros.Add("sp_ler", permissao.Ler);
                parametros.Add("sp_deletar", permissao.Excluir);


                var inserido = await _contexto
                     .Connection
                     .ExecuteAsync("sp_insert_permissao",
                     parametros,
                     commandType: System.Data.CommandType.StoredProcedure);

                return inserido;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
