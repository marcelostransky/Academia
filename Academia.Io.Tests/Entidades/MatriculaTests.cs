using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Io.Tests.Entidades
{
    [TestClass]
    public class MatriculaTests
    {
        private int _id;
        private int _idAluno;
        private int _totalParcelas;
        private DateTime _dataCriacao;
        private DateTime _dataContrato;
        private decimal _percentualDesconto;
        private decimal _valorDesconto;
        private decimal _valorMatricula;
        private decimal _valorContrato;
        private int _diaVencimento;
        private DateTime _dataInicialPagamento;
        private Guid _chaveRegistro;

        public MatriculaTests()
        {
            _id = 0;
            _idAluno = 9;
            _totalParcelas = 5;
            _dataCriacao = DateTime.Now;
            _dataContrato = DateTime.Now;
            _percentualDesconto = 10;
            _valorDesconto = 0;
            _valorMatricula = 0;
            _valorContrato = Convert.ToDecimal(1000.00);
            _diaVencimento = 5;
            _dataInicialPagamento = Convert.ToDateTime("20/07/2019");
            _chaveRegistro = Guid.NewGuid();
        }
        // Consigo criar uma nova turma
        [TestMethod]
        public void CriarMatriculaValida()
        {
            var matricula = new Matricula(_id, _idAluno, _totalParcelas,  _dataContrato,
                _percentualDesconto, _valorDesconto, _valorMatricula, _valorContrato, _diaVencimento,
                _dataInicialPagamento, _chaveRegistro,2019,1);
            Assert.AreEqual(true, matricula.Valid);
        }
        [TestMethod]
        public void DeveRetornar100ValorDesconto()
        {
            var matricula = new Matricula(_id, _idAluno, _totalParcelas, _dataContrato,
               _percentualDesconto, _valorDesconto, _valorMatricula, _valorContrato, _diaVencimento,
               _dataInicialPagamento, _chaveRegistro, 2019,1);
            Assert.AreEqual(100, matricula.ValorDesconto);
        }

        [TestMethod]
        public void DeveRetornar5Mensalidades()
        {
            var matricula = new Matricula(_id, _idAluno, _totalParcelas, _dataContrato,
              _percentualDesconto, _valorDesconto, _valorMatricula, _valorContrato, _diaVencimento,
              _dataInicialPagamento, _chaveRegistro, 2019,1);
            var retorno = 0;
                //matricula.GerarListaMensalidades();
            Assert.AreEqual(5, retorno);
        }
    }
}
