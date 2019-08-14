using AcademiaDanca.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query.Aluno;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Dapper;
using Slapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AcademiaDanca.IO.Infra.Repositorio
{
    public class AlunoRepositorio : IAlunoRepositorio
    {

        private readonly AcademiaContexto _contexto;

        public AlunoRepositorio(AcademiaContexto contexto)
        {
            _contexto = contexto;
        }

        public Task<bool> CheckCpfAsync(string cpf)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CheckFiliacaoAsync(Filiacao filiacao)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_email", filiacao.Email.Endereco);
                var id = (await _contexto
                      .Connection
                      .QueryAsync<int>("SELECT id FROM academia.filiacao where  email = @sp_email;",
                      parametros,
                      commandType: System.Data.CommandType.Text)).FirstOrDefault();

                return id;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<bool> CheckTurmaAlunoAsync(TurmaAluno turmaAluno)
        {
            var parametros = new DynamicParameters();
            parametros.Add("sp_id_aluno", turmaAluno.IdAluno);
            parametros.Add("sp_id_turma", turmaAluno.IdTurma);
            var existe = (await _contexto
                  .Connection
                  .QueryAsync<int>("SELECT Count(1) FROM academia.turma_aluno where id_aluno = @sp_id_aluno and id_turma = @sp_id_turma;",
                  parametros,
                  commandType: System.Data.CommandType.Text)).FirstOrDefault();

            return existe > 0;
        }

        public async Task<int> DeletarTurmaAluno(TurmaAluno turmaAluno)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id_turma", turmaAluno.IdTurma);
                parametros.Add("sp_id_aluno", turmaAluno.IdAluno);

                var total = await _contexto
                      .Connection
                      .ExecuteAsync("sp_delete_turma_aluno",
                      parametros,
                      commandType: System.Data.CommandType.StoredProcedure);

                return total;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> EditarFotoAsync(Aluno aluno)
        {
            var parametros = new DynamicParameters();

            parametros.Add("sp_id", aluno.Id);
            parametros.Add("sp_foto", aluno.Foto);
            var editado = await _contexto
                  .Connection
                  .ExecuteAsync("sp_edit_foto_aluno",
                  parametros,
                  commandType: System.Data.CommandType.StoredProcedure);

            return editado;
        }

        public Task<int> MatricularAssync(Matricula matricula)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Filiacao>> ObterFiliacaoAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Filiacao> ObterFiliacaoPorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Aluno> ObterPorAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<Aluno> ObterPorAsync(Guid uifId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AddResponsavelQuery>> ObterTipoFiliacaoAsync()
        {
            var query = @"SELECT id as Id, des_tipo_filiacao as Nome FROM academia.tipo_filiacao;";
            return await _contexto.Connection.QueryAsync<AddResponsavelQuery>(query, commandType: CommandType.Text);
        }

        public Task<IEnumerable<Aluno>> ObterTodosAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TotalTurmasQuery> ObterTotalTurmaAsync(Guid id)
        {
            var query = @"SELECT count(ta.id_turma) as Total, 
                         CAST(sum(t.valor) as decimal(7,2)) Valor FROM academia.turma as t
                         join academia.turma_aluno as ta on t.id = ta.id_turma
                         join academia.aluno as a on ta.id_aluno = a.id
                         where a.uif_id = @id";
            var parametros = new DynamicParameters();
            parametros.Add("id", id);

            return (await _contexto.Connection.QueryAsync<TotalTurmasQuery>(query, parametros, commandType: CommandType.Text)).FirstOrDefault();
        }

        public async Task<AlunoQuery> ObterAlunoCompletotesteAsync(Guid id)
        {
            try
            {
                var query = @"SELECT Distinct
                          a.id as AlunoId,
                          a.nome as AlunoNome,
                          a.email as AlunoEmail,
                          a.cpf as AlunoCpf,
                          a.foto as AlunoFoto,
                          a.data_nascimento as AlunoDataNascimento,
                          a.uif_id as AlunoGuid,
                          a.telefone as AlunoTelefone,
                          a.celular as AlunoCelular,
                          l.id as LogradouroId, 
                        l.rua as LogradouroRua, 
                        l.numero as LogradouroNumenro,
                        l.bairro as LogradouroBairro,
                        l.cep as LogradouroCep,
                        l.complemento as LogradouroComplemento,
                        l.cidade as LogradouroCidade,
                        l.estado as LogradouroEstado,
                        f.id as FiliacaoId,
                        f.nome as FiliacaoNome,
                        f.telefone as FiliacaoTelefone,
                        f.documento as FiliacaoDocumento,
                        f.email as FiliacaoEmail,
                        t.id as TurmaId,
                        t.des_turma as TurmaDescricao,
                        t.cod_turma as TurmaCodigo,
                        t.ano as TurmaAno,
                        t.valor as TurmaValor
                        FROM academia.logradouro as l
                        left join academia.logradouro_aluno as la on l.id = la.id_logradouro
                        left join academia.aluno as a on la.id_aluno = a.id
                        left join academia.aluno_filiacao as af on a.id = af.id_aluno
                        left join academia.filiacao as f on af.id_filiacao = f.id
                        left join academia.turma_aluno as ta on a.id = ta.id_aluno
                        left join academia.turma as t on ta.id_turma = t.id
                        left join academia.matricula as ma on a.id = ma.id_aluno
                        where a.uif_id=@id
                        Order by a.id,l.id,f.id,t.id;";
                var parametros = new DynamicParameters();
                parametros.Add("id", id);
                AlunoQuery aluno = new AlunoQuery();

                var alunoRetorno = (await _contexto.Connection.QueryAsync<AlunoQuery, AlunoEnderecoQuery, AlunoFiliacaoQuery, AlunoTurmaQuery, AlunoQuery>(query,
                    (a, l, f, t) =>
                    {
                        aluno = a;
                        aluno.AlunoTurmas.Add(t);
                        aluno.AlunoLogradouro = l;
                        //aluno.Alun.Add(f);
                        return aluno;
                    }, parametros, splitOn: "AlunoId,LogradouroId,FiliacaoId,TurmaId", commandType: CommandType.Text));
                return alunoRetorno.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AlunoQuery> ObterAlunoCompletoAsync(Guid id)
        {
            try
            {
                var query = @"SELECT Distinct
                          a.id as AlunoId,
                          a.nome as AlunoNome,
                          a.email as AlunoEmail,
                          a.cpf as AlunoCpf,
                          a.foto as AlunoFoto,
                          a.data_nascimento as AlunoDataNascimento,
                          a.uif_id as AlunoGuid,
                          a.telefone as AlunoTelefone,
                          a.celular as AlunoCelular,
                          l.id as AlunoLogradouro_LogradouroId, 
                          l.rua as AlunoLogradouro_LogradouroRua, 
                          l.numero as AlunoLogradouro_LogradouroNumenro,
                          l.bairro as AlunoLogradouro_LogradouroBairro,
                          l.cep as AlunoLogradouro_LogradouroCep,
                          l.complemento as AlunoLogradouro_LogradouroComplemento,
                          l.cidade as AlunoLogradouro_LogradouroCidade,
                          l.estado as AlunoLogradouro_LogradouroEstado,
                          f.id as AlunoFiliacoes_FiliacaoId,
                          f.nome as AlunoFiliacoes_FiliacaoNome,
                          f.telefone as AlunoFiliacoes_FiliacaoTelefone,
                          f.documento as AlunoFiliacoes_FiliacaoDocumento,
                          f.email as AlunoFiliacoes_FiliacaoEmail,
                          t.id as AlunoTurmas_TurmaId,
                          t.des_turma as AlunoTurmas_TurmaDescricao,
                          t.cod_turma as AlunoTurmas_TurmaCodigo,
                          t.ano as AlunoTurmas_TurmaAno,
                          t.valor as AlunoTurmas_TurmaValor,
                          ma.id as AlunoMatricula_MatriculaId,
                          ma.valor_contrato as AlunoMatricula_MatriculaValorContrato,
                          ma.percentual_desconto as AlunoMatricula_MatriculaPercentualDesconto,
                          ma.valor_desconto as AlunoMatricula_MatriculaValorDesconto,
                          ma.dia_venciamento as AlunoMatricula_MatriculaDiaVencimento,
                          ma.data_inicial_pagamento as AlunoMatricula_MatriculaDataInicialPagamento,
                          ma.uif_id as AlunoMatricula_MatriculaGuid,
                          ma.valor_matricula as AlunoMatricula_MatriculaValorMatricula,
                          ma.total_parcelas as AlunoMatricula_MatriculaTotalParcelas,
                          ma.ano as AlunoMatricula_MatriculaAno,
                          case when ma.status = 1 then 'Ativo' else 'Inativo' end as AlunoMatricula_MatriculaStatus,
                          me.id as AlunoMensalidades_MensalidadeId,
                          me.data_vencimento as AlunoMensalidades_MensalidadeDataVencimento,
                          me.valor as AlunoMensalidades_MensalidadeValor,
                          me.desconto as AlunoMensalidades_MensalidadeDesconto,
                          me.pago as AlunoMensalidades_MensalidadePago,
                          me.data_pagamento as AlunoMensalidades_MensalidadeDataPagamento,
                          me.juros as AlunoMensalidades_MensalidadeJuros,
                          me.parcela as AlunoMensalidades_MensalidadeParcela
                          FROM  academia.aluno as a
                          left join academia.logradouro_aluno as la on a.id = la.id_aluno
                          left join academia.logradouro as l on la.id_logradouro = l.id
                          left join academia.aluno_filiacao as af on a.id = af.id_aluno
                          left join academia.filiacao as f on af.id_filiacao = f.id
                          left join academia.turma_aluno as ta on a.id = ta.id_aluno
                          left join academia.turma as t on ta.id_turma = t.id
                          left join academia.matricula as ma on a.id = ma.id_aluno
                          left outer join academia.mensalidade as me on a.id = me.id_aluno and ma.id = me.id_matricula
                          where a.uif_id=@id
                          Order by a.id,l.id,f.id,t.id;";
                var parametros = new DynamicParameters();
                parametros.Add("id", id);
                AlunoQuery aluno = new AlunoQuery();

                var alunoRetorno = (await _contexto.Connection.QueryAsync<dynamic>(query,
                  parametros, commandType: CommandType.Text));
                AutoMapper.Configuration.AddIdentifier(typeof(AlunoQuery), "AlunoId");
                AutoMapper.Configuration.AddIdentifier(typeof(AlunoEnderecoQuery), "LogradouroId");
                AutoMapper.Configuration.AddIdentifier(typeof(AlunoFiliacaoQuery), "FiliacaoId");
                AutoMapper.Configuration.AddIdentifier(typeof(AlunoTurmaQuery), "TurmaId");
                AutoMapper.Configuration.AddIdentifier(typeof(AlunoMatriculaQuery), "MatriculaId");
                AutoMapper.Configuration.AddIdentifier(typeof(AlunoMensalidadeQuery), "MensalidadeId");
                aluno = (AutoMapper.MapDynamic<AlunoQuery>(alunoRetorno)).FirstOrDefault();
                return aluno;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<int> SalvarAsync(Aluno aluno)
        {
            var parametros = new DynamicParameters();
            parametros.Add("sp_id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("sp_nome", aluno.Nome);
            parametros.Add("sp_email", aluno.Email != null ? aluno.Email.Endereco : null);
            parametros.Add("sp_cpf", aluno.Cpf != null ? aluno.Cpf.Numero : null);
            parametros.Add("sp_foto", aluno.Foto);
            parametros.Add("sp_data_nascimento", aluno.DataNascimento);
            parametros.Add("sp_uif_id", aluno.UifId);
            parametros.Add("sp_telefone", aluno.Telefone);
            parametros.Add("sp_celular", aluno.Celular);
            await _contexto
                .Connection
                .ExecuteAsync("sp_insert_aluno",
                parametros,
                commandType: System.Data.CommandType.StoredProcedure);

            return parametros.Get<int>("sp_id");
        }



        public async Task<int> SalvarFiliacaoAsync(Filiacao filiacao)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("sp_nome", filiacao.Nome);
                parametros.Add("sp_telefone", filiacao.Telefone);
                parametros.Add("sp_email", filiacao.Email != null ? filiacao.Email.Endereco : null);
                parametros.Add("sp_documento", filiacao.Documento != null ? filiacao.Documento.Numero : null);
                parametros.Add("sp_id_tipo_filiacao", filiacao.IdTipoFiliacao);
                await _contexto
                    .Connection
                    .ExecuteAsync("sp_insert_filiacao",
                    parametros,
                    commandType: System.Data.CommandType.StoredProcedure);
                return parametros.Get<int>("sp_id");
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<int> SalvarFiliacaoAlunoAsync(int IdAluno, int IdFiliacao)
        {
            try
            {
                var parametros = new DynamicParameters();

                parametros.Add("sp_id_aluno", IdAluno);
                parametros.Add("sp_id_filiacao", IdFiliacao);

                var inserido = await _contexto
                    .Connection
                    .ExecuteAsync("sp_insert_filiacao_aluno",
                    parametros,
                    commandType: System.Data.CommandType.StoredProcedure);
                return inserido;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<int> SalvarTurmaAsync(TurmaAluno turmaAluno)
        {
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("sp_id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("sp_id_turma", turmaAluno.IdTurma);
                parametros.Add("sp_id_aluno", turmaAluno.IdAluno);

                var total = await _contexto
                      .Connection
                      .ExecuteAsync("sp_insert_turma_aluno",
                      parametros,
                      commandType: System.Data.CommandType.StoredProcedure);

                return total;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<IEnumerable<AlunoPorNomeQuery>> ObterTodosPorAsync(string nome)
        {
            var parametros = new DynamicParameters();
            parametros.Add("sp_nome", nome);

            var lista = await _contexto.Connection.QueryAsync<AlunoPorNomeQuery>
                ("sp_sel_aluno_por_nome", param: parametros, commandType: System.Data.CommandType.StoredProcedure);
            return lista;

        }

        public async Task<IEnumerable<AlunoPorNomeQuery>> ObterPorTurmaAsync(int idTurma)
        {
            var parametros = new DynamicParameters();
            parametros.Add("sp_id_turma", idTurma);


            var listaAluno = await _contexto
                  .Connection
                  .QueryAsync<AlunoPorNomeQuery>(@"
                                      SELECT A.email as Email, A.id as Id, A.uif_id as UifId, 
                                      A.nome as Nome, A.telefone as Telefone, A.celular as Celular, 
                                      A.data_nascimento as DataNascimento, A.foto as Foto 
                                      FROM academia.turma as T
                                      Join academia.turma_aluno as TA on T.id = TA.id_turma
                                      Join academia.aluno as A On TA.id_aluno = A.id
                                      where T.id = @sp_id_turma;",
                  parametros,
                  commandType: System.Data.CommandType.Text);

            return listaAluno;
        }
    }
}
