using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada.Com_Matricula;
using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query.Financeiro;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Financeiro.FIN_Matricula
{
    public class DelMatriculaItemManipulador : Notifiable, IComandoManipulador<DelMatriculaItemComando>
    {
        private readonly IFinanceiroRepositorio _repositorio;
        private readonly IConfiguracaoRepositorio _configuracao;
        public DelMatriculaItemManipulador(IFinanceiroRepositorio repositorio, IConfiguracaoRepositorio configuracao)
        {
            _repositorio = repositorio;
            _configuracao = configuracao;

        }
        public decimal ValorCalculado(decimal valor, int percentual)
        {
            percentual = percentual < 0 ? 0 : percentual;
            return valor - ((percentual * valor) / 100);
        }
        public async Task<IComandoResultado> ManipuladorAsync(DelMatriculaItemComando comando)
        {
            //Validar Comando
            AddNotifications(comando.Notifications);
            if (Invalid)
            {
                return new ComandoResultado(false, "Por favor, corrija os campos abaixo", Notifications);
            }
            //persistir dados
            //Deletar Tem item matricula
            var deletado = await _repositorio.DeletarItemMatriculaTemp(comando.IdMatricula, comando.IdTurma);

            //Atualizar Lista
            var turmas = (await _repositorio.ObterMatriculaItensTempPor(new Guid(comando.IdMatricula))).ToList();

            //Aplicar regra desconto
            var desconto = Convert.ToInt32((await _configuracao.ObterPorChaveAsync("DescontoCurso")).Valor);
               var idTurmaMaiorValor = 0;
            if (turmas != null && turmas.Count() > 0)
            {
                
                idTurmaMaiorValor = turmas.MaxBy(t => t.Valor).FirstOrDefault().IdTurma;

                foreach (var item in turmas)
                {
                    if (item.IdTurma != idTurmaMaiorValor)
                    {
                        item.ValorCalculado = ValorCalculado(item.Valor, desconto);
                        item.ValorDesconto = desconto;
                        item.Desconto = true;
                    }
                    else
                    {
                        item.ValorCalculado = item.Valor;
                        item.ValorDesconto = 0;
                        item.Desconto = false;
                    }
                }
            }

            //Atualizar Item    temp
            foreach (var item in turmas)
            {
                await _repositorio.AtualizaItemMatriculaTemp(new MatriculaItemTemp(
                item.IdMatriculaGuid,
                item.IdTurma,
                item.Valor,
                item.Desconto,
                item.ValorDesconto,
                item.ValorCalculado
                 ));

            }



            //Obter lista de retorno
            var itens = turmas;
            var totalGeral = itens.Sum(t => t.Valor);
            var totalDesconto = itens.Sum(t => t.ValorCalculado);
            // Retornar o resultado para tela
            return new ComandoResultado(true, "Item excluído com sucesso", new
            {
                itens,
                totalGeral,
                totalDesconto
            });
        }
    }
}
