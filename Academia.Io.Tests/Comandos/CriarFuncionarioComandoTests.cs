using AcademiaDanca.IO.Dominio.Contexto.Comandos.FuncionarioComando.Entrada;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Io.Tests.Comandos
{
    [TestClass]
    public class CriarFuncionarioComandoTests
    {
        [TestMethod]
        public void TrueQuandoComandoForValido()
        {
            var command = new CriarFuncionarioComando();
            command.Id = 0;
            command.Nome = "Marcelo Benes";
            command.Login = "Benes";
            command.DataNascimento = Convert.ToDateTime("20/10/2008");
            command.Cpf = "12345678900";
            command.Email = "M@M.com";
            command.Foto = "";
            command.Senha = "fsdsdf";
            command.ContraSenha = "fsdsdf";
            command.IdPerfil = 1;
            Assert.AreEqual(true, command.Valido());
        }
    }
}
