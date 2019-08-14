using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.FuncionarioComando.Entrada
{
    public class EditarAcessoFuncionarioComando : Notifiable, IComando
    {
        public int Id { get; set; }
        public string Senha { get; set; }
        public string ContraSenha { get; set; }



        public bool Valido()
        {
            AddNotifications(new ValidationContract()
               .IsTrue(Id > 0, "Codigo Funcionario", "Codigo não informado")
               .HasMinLen(Senha,4,"Senha","A senha precisa ter pelo menos 4 caracteres.")
               .IsNotNullOrEmpty(Senha, "Senha", "Senha não informada")
               .IsTrue(Senha.Equals(ContraSenha), "Senha", "Senha e contra senha informada não são identicas")
           );
            return Valid;
        }

    }
}
