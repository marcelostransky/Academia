using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.FuncionarioComando.Entrada
{
    public class EditarBaseFuncionarioComando : Notifiable, IComando
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string EmailAtual { get; set; }
        public string Cpf { get; set; }
        public string CpfAtual { get; set; }
        public int IdPerfil { get; set; }
        public string DescPerfil { get; set; }
        public string DescPerfilAtual { get; set; }

        public bool Valido()
        {
            AddNotifications(new ValidationContract()
               .IsBetween(DataNascimento, Convert.ToDateTime("01/01/1910"), DateTime.Now, "Data Nascimento", "Data informada não é válida")
               .IsTrue(Id > 0, "Codigo Funcionario", "Codigo não informado")
           );
            return Valid;
        }

    }
}
