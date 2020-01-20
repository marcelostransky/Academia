using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada.Com_Matricula;
using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using MoreLinq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Financeiro.FIN_Matricula
{
    public class MatricularManipulador : Notifiable, IComandoManipulador<MatricularComando>
    {
        private readonly IFinanceiroRepositorio _repositorio;
        private readonly IConfiguracaoRepositorio _configuracao;
        private readonly IAlunoRepositorio _repositorioAluno;

        public MatricularManipulador(IFinanceiroRepositorio repositorio, IAlunoRepositorio alunoRepositorio, IConfiguracaoRepositorio configuracao)
        {
            _repositorio = repositorio;
            _configuracao = configuracao;
            _repositorioAluno = alunoRepositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(MatricularComando comando)
        {

            //Obter itens matricula
            var turmas = (await _repositorio.ObterMatriculaItensTempPor(new Guid(comando.IdMatriculaGuid))).ToList();
            
            if (turmas.Count() <= 0)
            {
                AddNotification("Turmas", $"Informe pelo menos uma turma");
            }
            comando.ValorContrato = turmas.Sum(x => x.ValorCalculado);
            //Criar Entidade
            var matricula = new Matricula(comando.Id, comando.IdAluno, comando.TotalParcelas,
                comando.DataContrato, comando.PercentualDesconto, comando.ValorDesconto, comando.ValorMatricula,
               comando.ValorContrato, comando.DiaVencimento, comando.DataIncialPagamento, ChaveRegistro.Gerar(), comando.Ano, comando.MesInicioPagamento);

            //check Matricula Existe
            if (await _repositorio.CheckMatriculaExisteAsync(matricula))
                AddNotification("Matricula", $"Aluno informado ja possui matricula ativa para o ano de {comando.Ano} ");


            


            //Validar Comando
            comando.Valido();
            AddNotifications(comando.Notifications);
            if (Invalid)
            {
                return new ComandoResultado(false, "Por favor, corrija os campos abaixo", Notifications);
            }


            //Persistir Dados
            var id = await _repositorio.MatricularAsync(matricula);

            //Persistir Item
            foreach (var item in turmas)
            {
                await _repositorio.RegistrarItemMatricula(new MatriculaItem(
                 id,
                item.IdTurma,
                item.Valor,
                item.Desconto,
                item.ValorDesconto,
                item.ValorCalculado
                 ));
                //Enturmar Aluno
                await _repositorioAluno.SalvarTurmaAsync(new TurmaAluno(item.IdTurma, matricula.IdAluno));

            }
            //Deletar Tem item matricula
            await _repositorio.DeletarItemMatriculaTemp(comando.IdMatriculaGuid, 0);
            //Persistir Mensalidades
            await _repositorio.GerarMensalidade(new Mensalidade(0, matricula.IdAluno, id, matricula.TotalParcelas, Convert.ToDecimal(matricula.ValorContrato), matricula.ValorDesconto, matricula.DataIncialPagamento));

            // Retornar o resultado para tela
            return new ComandoResultado(true, "Matricula realizada com sucesso", new
            {
                Id = id,
                Nome = "Matriculado",
                Status = true
            });
        }
    }
}
