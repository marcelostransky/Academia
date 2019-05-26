using AcademiaDanca.IO.Dominio.Contexto.Comandos.TipoFiliacaoComando.Entrada;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;


namespace Academia.Io.Tests.Comandos
{
    [TestClass]
  public  class CriarTipoFiliacaoComandoTests
    {
        [TestMethod]
        public void TrueQuandoComandoForValido()
        {
            var command = new CriarTipoFiliacaoComando();
            command.DesTipoFiliacao = "Mae";
            Assert.AreEqual(true, command.Valido());
        }
    }
}
