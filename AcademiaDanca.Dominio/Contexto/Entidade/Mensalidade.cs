using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Entidade
{
    public class Mensalidade : Notifiable
    {
        public int IdAluno { get; private set; }
        public int Id { get; private set; }
        public int DiaPagamento { get; private set; }
        public int Parcela { get; private set; }
        public DateTime DataVencimento { get;private set; }
        public decimal Valor { get; private set; }
        public decimal Desconto { get; private set; }


        public Mensalidade(
            int id,
            int idAluno, 
            int parcela, 
            decimal valor, 
            decimal desconto, 
            DateTime dataVencimento)
        {
            Id = id;
            IdAluno = idAluno;
            Parcela = parcela;
            Valor = valor;
            Desconto = desconto;
            DataVencimento = dataVencimento;
        }

        public IList<Mensalidade> Mensalidades()
        {
            var mensalidadesRetorno = new List<Mensalidade>();
            var data = this.DataVencimento;
            for (int i = 1; i <= Parcela; i++)
            {
                mensalidadesRetorno.Add(new Mensalidade(Id, IdAluno, i, Valor, Desconto, data));
                data = data.AddMonths(1);
            }
            return mensalidadesRetorno;
        }
    }
}
