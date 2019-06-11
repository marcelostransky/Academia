using AcademiaDanca.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Infra.Repositorio
{
    public class EnderecoRepositorio : IEnderecoRepositorio
    {
        private readonly AcademiaContexto _contexto;
        public EnderecoRepositorio(AcademiaContexto contexto)
        {
            _contexto = contexto;
        }
        public Task<int> EditarFotoAsync(Endereco endereco)
        {
            throw new NotImplementedException();
        }

        public Task<Endereco> ObterPorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Endereco>> ObterTodosAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> SalvarAsync(Endereco endereco, int idAluno)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("sp_rua", endereco.Logradouro);
                parametros.Add("sp_numero", endereco.Numero);
                parametros.Add("sp_bairro", endereco.Bairro);
                parametros.Add("sp_cep", endereco.Cep);
                parametros.Add("sp_complemento", endereco.Complemento);
                parametros.Add("sp_cidade", endereco.Cidade);
                parametros.Add("sp_estado", endereco.Estado.Sigla);
                parametros.Add("sp_id_aluno", idAluno);
                await _contexto
                    .Connection
                    .ExecuteAsync("sp_insert_logradouro_aluno",
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
