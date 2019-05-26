using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.Aluno.Entrada;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.AlunoContexto
{
    public class AddEnderecoManipulador : Notifiable, IComandoManipulador<CriarAlunoComando>
    {
        public Task<IComandoResultado> ManipuladorAsync(CriarAlunoComando comando)
        {
            throw new NotImplementedException();
        }
    }
}
