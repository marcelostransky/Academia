using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.FuncionarioComando.Entrada
{
    public class CriarFuncionarioComando : Notifiable, IComando
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Foto { get; set; }
        public string  Login { get; set; }
        public string Senha { get; set; }
        public string ContraSenha { get; set; }
        public int IdPerfil { get; set; }

        public bool Valido()
        {
            AddNotifications(new ValidationContract()
              
               .HasMinLen(Login, 3, "login", "O Login deve conter pelo menos 3 caracteres")
               .HasMaxLen(Login, 300, "login", "O Login deve conter no máximo 300 caracteres")
               .HasMinLen(Senha, 3, "senha", "Senha deve conter pelo menos 6 caracteres")
               .HasMaxLen(Senha, 300, "senha", "Senha deve conter no máximo 12 caracteres")
               .IsBetween(DataNascimento, Convert.ToDateTime("01/01/1930"), DateTime.Now, "Data Nascimento", "Data informada não é válida")
               .IsTrue(IdPerfil > 0, "Perfil","Perfil não informado")
               .IsTrue(Senha.Equals(ContraSenha),"Contra Senha","Contra senha informada não é válida")
           );
            return Valid;
        }
    }
}
