using AcademiaDanca.IO.Dominio.Contexto.Comandos.TipoTelefoneComando.Entrada;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Io.Tests.Comandos
{
    [TestClass]
   public class CreateTipoTelefoneComandoTests
    {
        [TestMethod]
        public void TrueQuandoComandoForValido()
        {
            var command = new CreateTipoTelefoneComando();
            command.DesTipoTelefone = "Residencial";
            Assert.AreEqual(true, command.Valido());
        }
    }
}
