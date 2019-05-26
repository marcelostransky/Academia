using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Compartilhado.Comando
{
    public interface IComandoManipulador<T> where T : IComando
    {
        Task<IComandoResultado> ManipuladorAsync(T comando);
    }
}
