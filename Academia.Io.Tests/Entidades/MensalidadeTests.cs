using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Io.Tests.Entidades
{
    [TestClass]
    public class MensalidadeTests
    {
        int Dia = 5;
        Mensalidade mensalidade = new Mensalidade(0, 1, 5, Convert.ToDecimal(568.32), 20, Convert.ToDateTime("30/03/2019"));
        [TestMethod]
        public void DeveRetornar12datasVencimentoPorDia()
        {
            DateTime DataInicial = Convert.ToDateTime($"{Dia}/03/2019");
            int TotalParcelas = 12;
            List<DateTime> Datas = new List<DateTime>();
            for (int i = 1; i <= TotalParcelas; i++)
            {
                Datas.Add(DataInicial);
                DataInicial = DataInicial.AddMonths(1);
            }
            Assert.AreEqual(12, Datas.Count);
        }

        [TestMethod]
        public void DeveRetornar5Mensalidades()
        {
            var retorno = mensalidade.Mensalidades();
            Assert.AreEqual(5, retorno.Count);
        }
    }
}
