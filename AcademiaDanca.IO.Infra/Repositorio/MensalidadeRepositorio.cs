using AcademiaDanca.IO.Compartilhado.Util;
using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query.Financeiro;
using AcademiaDanca.IO.Dominio.Contexto.Query.Financeiro.Mensalidade;
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
    public class MensalidadeRepositorio : IMensalidadeRepositorio
    {
        private readonly AcademiaContexto _contexto;
        public MensalidadeRepositorio(AcademiaContexto contexto)
        {
            _contexto = contexto;
        }
        public Task<int> DeletarAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<List<string>> ObterListaAnoDataVencimento()
        {
            try
            {
                var query = @"SELECT distinct year(data_vencimento) as Ano FROM academia.mensalidade;";
                var anos = await _contexto.Connection.QueryAsync<string>(
                       query, commandType: CommandType.Text);
                return anos.ToList();
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
        public async Task<List<TipoMensalidadeQueryResultado>> ObterTipoMensalidadeAsync()
        {
            try
            {
                var query = @"SELECT * FROM academia.tipo_mensalidade;";
                var tipos = await _contexto.Connection.QueryAsync<TipoMensalidadeQueryResultado>(
                       query, commandType: CommandType.Text);
                return tipos.ToList();
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
        public Task<int> EditarAsync(Mensalidade mensalidade)
        {
            throw new NotImplementedException();
        }

        public Task<MensalidadesQueryResultado> ObterMensalidadePorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MensalidadesQueryResultado>> ObterMensalidadesAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<int> AtualizarValorAsync(int idMatricula, decimal valor)
        {
            try
            {
                var parametro = new DynamicParameters();
                parametro.Add("sp_id_matricula", idMatricula);
                parametro.Add("sp_valor", valor);
                parametro.Add("sp_data", Data.ObterDataFormatoUnix(DateTime.Now));
                var query = @"update academia.mensalidade set valor = @sp_valor  where id_matricula = @sp_id_matricula and id_tipo_mensalidade = 2 and data_vencimento > @sp_data;";
                var atualizado = await _contexto.Connection.ExecuteAsync(
                       query, param: parametro, commandType: CommandType.Text);
                return atualizado;
            }
            catch(Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar os valores da mensalidade");
            }
            finally
            {
                _contexto.Dispose();
            }
        }
        public Task<int> SalvarAsync(Mensalidade mensalidade)
        {
            throw new NotImplementedException();
        }

    }
}
