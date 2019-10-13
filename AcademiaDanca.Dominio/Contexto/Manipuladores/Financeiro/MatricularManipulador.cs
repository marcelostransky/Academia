using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using System;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Financeiro
{
    public class MatricularManipulador : Notifiable, IComandoManipulador<MatricularComando>
    {
        private readonly IFinanceiroRepositorio _repositorio;
        public MatricularManipulador(IFinanceiroRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(MatricularComando comando)
        {

            //Criar Entidade
            var matricula = new Matricula(comando.Id, comando.IdAluno, comando.TotalParcelas,
                comando.DataContrato, comando.PercentualDesconto, comando.ValorDesconto, comando.ValorMatricula,
               Convert.ToDecimal(comando.ValorContrato), comando.DiaVencimento, comando.DataIncialPagamento, ChaveRegistro.Gerar(), comando.Ano);

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
