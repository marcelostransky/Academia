using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Entidade
{
    public class Credencial: Notifiable
    {
        public Credencial(int id, string login, string senha)
        {
            Id = id;
            Login = login;
            Senha = senha;
        }
        public int Id { get; private set; }
        public string Login { get; private set; }
        public string Senha { get; private set; }


    }
}
