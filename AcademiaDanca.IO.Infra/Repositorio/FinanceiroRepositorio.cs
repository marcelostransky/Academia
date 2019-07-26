using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Infra.Repositorio
{
    public class FinanceiroRepositorio : IFinanceiroRepositorio
    {
        private readonly AcademiaContexto _contexto;

        public FinanceiroRepositorio(AcademiaContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> CheckMatriculaExisteAsync(Matricula matricula)
        {
            var parametros = new DynamicParameters();
            parametros.Add("sp_id_aluno", matricula.IdAluno);
            parametros.Add("sp_ano", matricula.Ano);
            var existe = (await _contexto
                  .Connection
                  .QueryAsync<int>("SELECT count(1) FROM academia.matricula where id_aluno = @sp_id_aluno and ano = @sp_ano",
                  parametros,
                  commandType: System.Data.CommandType.Text)).FirstOrDefault();

            return existe > 0;
        }
        public async Task<int> MatricularAsync(Matricula matricula)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("sp_id_aluno", matricula.IdAluno);
                parametros.Add("sp_valor_contrato", matricula.ValorContrato);
                parametros.Add("sp_percentual_desconto", matricula.PercentualDesconto);
                parametros.Add("sp_valor_desconto", matricula.ValorDesconto);
                parametros.Add("sp_dia_vencimento", matricula.DiaVencimento);
                parametros.Add("sp_data_inicial_pagamento", matricula.DataIncialPagamento);
                parametros.Add("sp_uif_id", matricula.ChaveRegistro);
                parametros.Add("sp_valor_matricula", matricula.ValorMaricula);
                parametros.Add("sp_data_contrato", matricula.DataContrato);
                parametros.Add("sp_data_criacao", matricula.DataCriacao);
                parametros.Add("sp_total_parcelas", matricula.TotalParcelas);
                parametros.Add("sp_ano", matricula.Ano);
                await _contexto
                    .Connection
                    .ExecuteAsync("sp_insert_matricula",
                    parametros,
                    commandType: System.Data.CommandType.StoredProcedure);

                return parametros.Get<int>("sp_id");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task GerarMensalidade(Mensalidade mensalidade)
        {
            var lista = mensalidade.Mensalidades();
            foreach (var mes in lista)
            {
                try
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("sp_id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    parametros.Add("sp_id_matricula", mensalidade.IdMatricula);
                    parametros.Add("sp_data_vencimento", mensalidade.DataVencimento);
                    parametros.Add("sp_valor", mensalidade.Valor);
                    parametros.Add("sp_desconto", mensalidade.Desconto);
                    parametros.Add("sp_parcela", mensalidade.Parcela);
                    parametros.Add("sp_id_aluno", mensalidade.IdAluno);
                    
                    await _contexto
                        .Connection
                        .ExecuteAsync("sp_insert_mensalidade",
                        parametros,
                        commandType: System.Data.CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
