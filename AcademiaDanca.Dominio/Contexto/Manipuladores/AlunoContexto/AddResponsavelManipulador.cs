using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.AlunoComando.Entrada;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.AlunoContexto
{
    public class AddResponsavelManipulador : Notifiable, IComandoManipulador<AddFiliacaoComando>
    {
        public Task<IComandoResultado> ManipuladorAsync(AddFiliacaoComando comando)
        {
            throw new NotImplementedException();
        }
    }
}
