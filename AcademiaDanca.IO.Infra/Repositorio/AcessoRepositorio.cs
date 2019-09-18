﻿using AcademiaDanca.IO.Dominio.Contexto.Entidade;
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

        public async Task<bool> CheckPermissaoAsync(int paginaId, int perfilId)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_pagina_id", paginaId);
                parametros.Add("sp_papel_id", perfilId);
                var total = (await _contexto
                      .Connection
                      .QueryAsync<int>("SELECT count(1) FROM academia.pagina_papel where id_pagina = @sp_pagina_id and id_papel=@sp_papel_id;",
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

        public async Task<IEnumerable<PermissaoResultadoQuery>> ObterPermissaosAsync(
            string paginaId, string perfilId)
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
                        pg.id as PaginaId,
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
                        pg.id = ifnull(@sp_id_pagina, pg.id)
                        And
                        p.id = ifnull(@sp_id_perfil, p.id);", param: parametros,

                        commandType: System.Data.CommandType.Text);
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
