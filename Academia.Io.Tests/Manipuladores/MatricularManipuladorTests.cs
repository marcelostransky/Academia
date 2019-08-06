using Academia.Io.Tests.Fakes;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Financeiro;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Academia.Io.Tests.Manipuladores
{
    [TestClass]
    public class MatricularManipuladorTests
    {
        [TestMethod]
        public async Task DeveRegistrarUmaMatriculaQuandoComandoForValidoAsync()
        {
            var comando = new MatricularComando
            {
                Id = 0,
                IdAluno = 10,
                DataContrato = DateTime.Now,
                TotalParcelas = 5,
                PercentualDesconto = 10,
                ValorDesconto = Convert.ToDecimal("0"),
                ValorMaricula = 0,
                ValorContrato = Convert.ToDouble("560,00"),
                DiaVencimento = 1,
                Ano = 2019,
                DataIncialPagamento = DateTime.Now,
                ChaveRegistro = Guid.NewGuid()


            };
            var manipulador = new MatricularManipulador(new FakeFinanceiroRepositorio());
            var result = await manipulador.ManipuladorAsync(comando);
            Assert.AreNotEqual(null, result);
            Assert.AreEqual(true, manipulador.Valid);
        }
    }
}
