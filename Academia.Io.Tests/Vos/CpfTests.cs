using AcademiaDanca.IO.Dominio.Contexto.Vo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Academia.Io.Tests.Vos
{
    [TestClass]
    public class CpfTests
    {
        private Cpf validoCpf;
        private Cpf invalidoCpf;

        public CpfTests()
        {
            validoCpf = new Cpf("28659170377");
            invalidoCpf = new Cpf("12345678910");
        }

        [TestMethod]
        public void RetornaNotificacaoQuandoDocumentoNaoForValido()
        {
            Assert.AreEqual(false, invalidoCpf.Valid);
            Assert.AreEqual(1, invalidoCpf.Notifications.Count);
        }

        [TestMethod]
        public void RetornaTrueQuandoDocumentoForValido()
        {
            Assert.AreEqual(true, validoCpf.Valid);
            Assert.AreEqual(0, validoCpf.Notifications.Count);
        }
    }
}
