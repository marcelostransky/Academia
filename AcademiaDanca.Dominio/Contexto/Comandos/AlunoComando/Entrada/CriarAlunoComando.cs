using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.Aluno.Entrada
{
    public class CriarAlunoComando : Notifiable, IComando
    {
        public int Id { get;  set; }
        public Guid UifId { get; set; }
        public string Nome { get;  set; }
        public DateTime DataNascimento { get;  set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Foto { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }

        public bool Valido()
        {
            AddNotifications(new ValidationContract()
               .HasMinLen(Nome, 3, "Nome", "O nome deve conter pelo menos 3 caracteres")
               .HasMaxLen(Nome, 300, "Nome", "O nome deve conter no máximo 300 caracteres")
               .HasMaxLen(Foto, 300, "Foto", "O nome deve conter no máximo 300 caracteres")
               .IsEmail(Email, "Email", "O E-mail é inválido")
               .IsBetween(DataNascimento, Convert.ToDateTime("01/01/1930"), DateTime.Now, "Data Nascimento", "Data informada não é válida")
               .HasMaxLen(Cpf, 11, "Cpf", "CPF inválido")
           );
            return Valid;
        }


    }
}
