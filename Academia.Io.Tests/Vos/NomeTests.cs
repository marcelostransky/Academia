using AcademiaDanca.IO.Dominio.Contexto.Vo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Io.Tests.Vos
{
    [TestClass]
    public class NomeTests
    {
        [TestMethod]
        public void RetornaNotificacaoQuandoNomeNaoForValido()
        {
            var nome = new Nome("");
            Assert.AreEqual(false, nome.Valid);
            Assert.AreEqual(2, nome.Notifications.Count);
        }
    }
}
