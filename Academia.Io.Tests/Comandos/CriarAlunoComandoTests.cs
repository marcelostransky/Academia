using AcademiaDanca.IO.Dominio.Contexto.Comandos.Aluno.Entrada;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Academia.Io.Tests.Comandos
{
    [TestClass]
    public class CriarAlunoComandoTests
    {
        [TestMethod]
        public void TrueQuandoComandoForValido()
        {
            var comando = new CriarAlunoComando();
            comando.Id = 11;
            comando.UifId = Guid.NewGuid();
            comando.Nome = "Marcelo Benes Stransky Silva";
            comando.Email = "Marcelo@marcelo.com";
            comando.Foto = "/fff/f.png";
            comando.Cpf = "99081598600";
            comando.DataNascimento = Convert.ToDateTime("30/03/1977");
            comando.Telefone = "11999849077";
            comando.Celular = "11999876598";
            Assert.AreEqual(true, comando.Valido());
        }
    }
}
