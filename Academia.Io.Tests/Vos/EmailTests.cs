using AcademiaDanca.IO.Dominio.Contexto.Vo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Io.Tests.Vos
{
    [TestClass]
    public class EmailTests
    {
        [TestMethod]
        public void RetornaNotificacaoQuandoEmailNaoForValido()
        {
            var email = new Email("dsfsdf");
            Assert.AreEqual(false, email.Valid);
            Assert.AreEqual(1, email.Notifications.Count);
        }
        [TestMethod]
        public void RetornaTrueQuandoEmailForValido()
        {
            var email = new Email("m@m.com");
            Assert.AreEqual(true, email.Valid);
        }
    }
}
