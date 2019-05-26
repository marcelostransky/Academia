using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.App.Models
{
     public class LoginModel
    {
        public LoginModel(string login, string senha)
        {
            Login = login;
            Senha = senha;
        }
         public string Login { get; set; }
         public string Senha { get;  set; }
    }
}
