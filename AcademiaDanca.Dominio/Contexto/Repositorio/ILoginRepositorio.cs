using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Repositorio
{
   public interface ILoginRepositorio
    {
        Task<bool> CheckLoginAsync(string login);
        Task<int> SalvarAsync(Credencial funcionario);
       
        Task<AutenticarFuncionarioQueryResultado> Autenticar(Credencial credencial);
    }
}
