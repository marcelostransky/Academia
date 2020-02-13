using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada.Com_Matricula;
using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query.Financeiro;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Financeiro.FIN_Matricula
{
    public class EditMatriculaManipulador : Notifiable, IComandoManipulador<EditMatriculaComando>
    {
        private readonly IFinanceiroRepositorio _repositorio;
        private readonly IConfiguracaoRepositorio _configuracao;
        private readonly IAlunoRepositorio _repositorioAluno;
        private readonly IMatriculaRepositorio _repositorioMatricula;
        private readonly ITurmaRepositorio _repositorioTurma;
        private readonly IMensalidadeRepositorio _repositorioMensalidade;

        public EditMatriculaManipulador(IFinanceiroRepositorio repositorio, IAlunoRepositorio alunoRepositorio,
            IConfiguracaoRepositorio configuracao,
            IMatriculaRepositorio matriculaRepositorio,
            ITurmaRepositorio turmaRepositorio,
            IMensalidadeRepositorio mensalidadeRepositorio
            )
        {
            _repositorio = repositorio;
            _configuracao = configuracao;
            _repositorioAluno = alunoRepositorio;
            _repositorioMatricula = matriculaRepositorio;
            _repositorioTurma = turmaRepositorio;
            _repositorioMensalidade = mensalidadeRepositorio;
        }

        public List<ItemMatriculaQueryResultado> ObterDiferencaLista(List<ItemMatriculaQueryResultado> lista, List<ItemMatriculaQueryResultado> listaEdit)
        {
            var listaRetorno = new List<ItemMatriculaQueryResultado>();

            foreach (var item in lista)
            {
                if (!listaEdit.Exists(x => x.IdTurma == item.IdTurma))
                    listaRetorno.Add(item);
            }

            return listaRetorno;
        }
        public async Task<IComandoResultado> ManipuladorAsync(EditMatriculaComando comando)
        {
            //Recuperar Matricula
            var matricula = await _repositorioMatricula.ObterPor(new Guid(comando.IdMatriculaGuid));

            if (!matricula.Status)
                AddNotification("Matricula", "Matricula ja esta inativada. Operação não realizada.");
            
            //Validar Comando
            comando.Valido();

            //Recuperar itens matricula modificado
            var turmasTemp = (await _repositorio.ObterMatriculaItensTempPor(new Guid(comando.IdMatriculaGuid))).ToList();

            //Verificar total trumas > 0
            if (turmasTemp.Count <= 0)
                AddNotification("Item", $"Informe Pelo Menos Uma Turma");

            //Recupera itens matricula
            var turmas = (await _repositorio.ObterMatriculaItensPor(new Guid(comando.IdMatriculaGuid))).ToList();

            //Obter as turmas excluidas para inativar o aluno
            var turmasExclusao = ObterDiferencaLista(turmas, turmasTemp);

            AddNotifications(comando.Notifications);
            if (Invalid)
            {
                return new ComandoResultado(false, "Por favor, corrija os campos abaixo", Notifications);
            }

            if (comando.StatusMatricula)
            {
                //Deletar turmas 
                var deletado = await _repositorioMatricula.DeletarItemMatricula(matricula.IdMatricula, 0);

                //Excluir aluno da turma
                if (deletado > 0 && turmasExclusao.Count > 0)
                {
                    foreach (var item in turmasExclusao)
                        await _repositorioTurma.DeletarAlunoAsync(matricula.IdAluno, item.IdTurma);
                }

                //Persistir Item
                foreach (var item in turmasTemp)
                {
                    await _repositorio.RegistrarItemMatricula(new MatriculaItem(
                     matricula.IdMatricula,
                    item.IdTurma,
                    item.Valor,
                    item.Desconto,
                    item.ValorDesconto,
                    item.ValorCalculado
                     ));
                    //Enturmar Aluno
                    await _repositorioAluno.SalvarTurmaAsync(new TurmaAluno(item.IdTurma, matricula.IdAluno));

                }

                var novoValorMensalidade = turmasTemp.Sum(x => x.ValorCalculado);

                //Atualiza Mensalidade
                await _repositorioMensalidade.AtualizarValorAsync(matricula.IdMatricula, novoValorMensalidade);

            }
            else if (!comando.StatusMatricula)
            {
                //Inativar matricula
                if (await _repositorioMatricula.InativarAsync(matricula.IdMatricula) > 0)
                {
                    //Inativar Turma
                    foreach (var item in turmas)
                        await _repositorioTurma.DeletarAlunoAsync(matricula.IdAluno, item.IdTurma);

                    //Inativar Pagamentos Pendente

                }

            }

            return new ComandoResultado(true, "Item Atualizado com Sucesso", new
            {
                Id = matricula.IdMatricula,
                Nome = "Atualizado",
                Status = true
            });
        }
    }
}
