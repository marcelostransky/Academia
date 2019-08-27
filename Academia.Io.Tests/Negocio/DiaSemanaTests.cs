using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

using AcademiaDanca.IO.Compartilhado.Util;

namespace Academia.Io.Tests.Negocio
{
    [TestClass]
    public class DiaSemanaTests
    {
        [TestMethod]
        public void DeveRetornarDiaSemanaPordataInformada()
        {

            var culture = new System.Globalization.CultureInfo("pt-Br");
            var dayFalse =Convert.ToDateTime("15/08/2019").GetDayOfWeek(culture);
            var dayTrue = DateTime.Now.GetDayOfWeek(culture);
            //int num =Convert.ToInt32(DateTime.Now.GetDayOfWeek(culture));
            var dia = DateTime.Now.DayOfWeek;

            Assert.AreEqual(dayTrue, "sábado");
            Assert.AreNotEqual(dayFalse, "sábado");
        }


    }
}
