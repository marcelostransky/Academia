using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada.Com_Matricula;
using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query.Financeiro;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Financeiro.FIN_Matricula
{
    public class ItemMatriculaManipulador : Notifiable, IComandoManipulador<MatriculaItemComando>
    {
        private readonly IFinanceiroRepositorio _repositorio;
        private readonly IConfiguracaoRepositorio _configuracao;
        public ItemMatriculaManipulador(IFinanceiroRepositorio repositorio, IConfiguracaoRepositorio configuracao)
        {
            _repositorio = repositorio;
            _configuracao = configuracao;
        }
        public decimal ValorCalculado(decimal valor, int percentual)
        {
            percentual = percentual < 0 ? 0 : percentual;
            return valor - ((percentual * valor) / 100);
        }
        public async Task<IComandoResultado> ManipuladorAsync(MatriculaItemComando comando)
        {


            //check Matricula Existe
            if (await _repositorio.CheckMatriculaItemTempExisteAsync(comando))
                AddNotification("Item", $"Item já informado");

            //Aplicar regra desconto
            var desconto = Convert.ToInt32((await _configuracao.ObterPorChaveAsync("DescontoCurso")).Valor);
            var turmas = (await _repositorio.ObterMatriculaItensTempPor(new Guid(comando.IdMatriculaGuid))).ToList();
            var idTurmaMaiorValor = 0;
            if (turmas != null && turmas.Count() > 0)
            {
                turmas.Add(new ItemMatriculaQueryResultado
                {
                    Desconto = false,
                    IdMatriculaGuid = comando.IdMatriculaGuid,
                    IdTurma = comando.IdTurma,
                    Valor = comando.Valor,
                    ValorDesconto = 0,
                    ValorCalculado = comando.Valor,
                    ValorTotalDesconto = comando.Valor
                });
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
            else
            {
                turmas = new List<ItemMatriculaQueryResultado>();
                turmas.Add(new ItemMatriculaQueryResultado
                {
                    Desconto = false,
                    IdMatriculaGuid = comando.IdMatriculaGuid,
                    IdTurma = comando.IdTurma,
                    Valor = comando.Valor,
                    ValorDesconto = 0,
                    ValorCalculado = comando.Valor,
                    ValorTotalDesconto = comando.Valor
                });
            }
            //Validar Comando
            comando.Valido();
            AddNotifications(comando.Notifications);
            if (Invalid)
            {
                return new ComandoResultado(false, "Por favor, corrija os campos abaixo", Notifications);
            }


            //Persistir Item    temp
            foreach (var item in turmas)
            {
                await _repositorio.RegistrarItemMatriculaTemp(new MatriculaItemTemp(
                item.IdMatriculaGuid,
                item.IdTurma,
                item.Valor,
                item.Desconto,
                item.ValorDesconto,
                item.ValorCalculado
                 ));

            }


            //Obter lista de retorno
            var itens = (await _repositorio.ObterMatriculaItensTempPor(new Guid(comando.IdMatriculaGuid))).ToList();
            var totalGeral = itens.Sum(t => t.Valor);
            var totalDesconto = itens.Sum(t => t.ValorCalculado);
            // Retornar o resultado para tela
            return new ComandoResultado(true, "Item incluido com sucesso", new
            {
                itens,
                totalGeral,
                totalDesconto
            });
        }
    }
}
