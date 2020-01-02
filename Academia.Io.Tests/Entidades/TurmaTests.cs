using AcademiaDanca.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Vo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Io.Tests.Entidades
{
    [TestClass]
    public class TurmaTests
    {
        private int _id;
        private int _ano;
        private string _desTurma;
        private Funcionario _professor;
        private TurmaTipo _turmaTipo;
        private Turma _turma;

        public TurmaTests()
        {
            var nome = new Nome("Marcelo Benes Stransky Silva");
            var email = new Email("marcelo@gmail.com");
            var cpf = new Cpf("99081598600");
            var dataNascimento = DateTime.Now;
            _turmaTipo = new TurmaTipo(0, "Dança", 0);
            _professor = new Funcionario(0, nome, email, cpf, dataNascimento);
           
            _turma = new Turma(0, "Turma A","", _professor, _turmaTipo, true);
            _turma.AddNotifications(nome);
            _turma.AddNotifications(email);
            _turma.AddNotifications(cpf);

            _ano = 2019;
        }

        // Consigo criar uma nova turma
        [TestMethod]
        public void CriarUmaTurmaValida()
        {
            Assert.AreEqual(true, _turma.Valid);
        }

        // Ao adicionar um novo aluno, a quantidade de alunos deve mudar
        [TestMethod]
        public void DeveRetornar2quandoAdicionado2Alunos()
        {
            _turma.AddAluno(
                new Aluno(0, "Marcelo",  
                DateTime.Now,
                new Endereco("sdsada", 0, "1220","xzczxc", "bairro", "alagoas","02935090", new Uf(0, "Minas", "MG")), 
                new Email("marcelo@gmail.com")));
            _turma.AddAluno(
               new Aluno(0, "Marcelogggg",
               DateTime.Now,
               new Endereco("sdsada", 0, "1220","fsdfsdf","bairro", "alagoas", "02935090", new Uf(0, "Minas", "MG")),
               new Email("marcelo@gmail.com")));
            Assert.AreEqual(2, _turma.Alunos.Count);
        }
    }
}
