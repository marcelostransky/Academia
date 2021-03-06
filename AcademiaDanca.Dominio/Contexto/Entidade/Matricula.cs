﻿using AcademiaDanca.Dominio.Contexto.Entidade;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Entidade
{
    public class Matricula : Notifiable
    {
        private readonly IList<Turma> _turmas;
        public int Id { get; private set; }
        public int IdAluno { get; private set; }
        public Aluno Aluno { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime DataContrato { get; private set; }
        public decimal PercentualDesconto { get; private set; }
        public decimal ValorDesconto { get; private set; }
        public decimal ValorMaricula { get; private set; }
        public decimal ValorContrato { get; private set; }
        public int DiaVencimento { get; private set; }
        public int TotalParcelas { get; private set; }
        public int Ano { get; private set; }
        public int MesInicioPagamento { get; private set; }
        public DateTime DataIncialPagamento { get; private set; }
  
        public Guid ChaveRegistro { get; private set; }

        public Matricula(
            int id,
            int idAluno,
            int totalParcelas,
            DateTime dataContrato,
            decimal percentualDesconto,
            decimal valorDesconto,
            decimal valorMatricula,
            decimal valorContrato,
            int diaVencimento,
            DateTime dataInicialPagamento,
            Guid chaveRegistro,
            int ano ,
            int mesInicioPagamento 
            
            )
        {
            Id = id;
            IdAluno = idAluno;
            DataCriacao = DateTime.Now;
            DataContrato = dataContrato;
            PercentualDesconto = percentualDesconto;
            ValorMaricula = valorMatricula;
            ValorContrato = valorContrato;
            DiaVencimento = diaVencimento;
            DataIncialPagamento = dataInicialPagamento;
            ChaveRegistro = chaveRegistro;
            TotalParcelas = totalParcelas;
            ValorDesconto = ObterValorComDesconto(valorDesconto);
            Ano = ano;
            MesInicioPagamento = mesInicioPagamento;
            List<Turma> turmas = new List<Turma>();
            
        }
        public void AddTurma(Turma turma)
        {
            _turmas.Add(turma);
        }
        private decimal ObterValorComDesconto(decimal valorDesconto)
        {
            if (PercentualDesconto > 0)
                valorDesconto = (PercentualDesconto / 100 * ValorContrato);
            return valorDesconto;
        }
        //public IList<Mensalidade> GerarListaMensalidades()
        //{
        //    var mensalidadesRetorno = new List<Mensalidade>();
        //    var data = this.DataIncialPagamento;
        //    for (int i = 1; i <= this.TotalParcelas; i++)
        //    {
        //        mensalidadesRetorno.Add(new Mensalidade(Id, IdAluno, Id, i, ValorContrato, ValorDesconto, data, TipoMatricula));
        //        data = data.AddMonths(1);
        //    }
        //    return mensalidadesRetorno;
        //}
    }
}
