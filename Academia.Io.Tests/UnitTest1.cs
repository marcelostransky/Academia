using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreLinq;
using System.Collections.Generic;
using System.Linq;

namespace Academia.Io.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var desconto = 50;
            var lista = new List<MatriculaItemComando>();
            lista.Add(new MatriculaItemComando
            {
                IdMatricula = 1,
                IdTurma = 2,
                Valor = 100,
                AplicarDesconto = false
            });
            lista.Add(new MatriculaItemComando
            {
                IdMatricula = 2,
                IdTurma = 3,
                Valor = 500,
                AplicarDesconto = false
            });
            lista.Add(new MatriculaItemComando
            {
                IdMatricula = 6,
                IdTurma = 8,
                Valor = 500,
                AplicarDesconto = false
            });
            lista.Add(new MatriculaItemComando
            {
                IdMatricula = 3,
                IdTurma = 4,
                Valor = 50,
                AplicarDesconto = false
            });

            var idTurmaMaiorValor = lista.MaxBy(t => t.Valor).FirstOrDefault().IdTurma;

            foreach(var item in lista)
            {
                if(item.IdTurma != idTurmaMaiorValor)
                {
                    item.ValorCalculado = ValorCalculado(item.Valor, desconto);
                    item.ValorDesconto = desconto;
                    item.AplicarDesconto = true;
                }
                else
                {
                    item.ValorCalculado = item.Valor;
                    item.ValorDesconto = 0;
                    item.AplicarDesconto = false;
                }
            }

            var novaLista = lista;

        }

        public decimal ValorCalculado(decimal valor, int percentual)
        {
            percentual = percentual < 0 ? 0 : percentual;
            return valor - ((percentual * valor) / 100);
        }
    }
}
