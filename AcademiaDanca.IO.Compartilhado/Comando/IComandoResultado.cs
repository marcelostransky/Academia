using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Compartilhado.Comando
{
   public interface IComandoResultado
    {
       
        bool Success { get; set; }
        string Message { get; set; }
        object Data { get; set; }
    }
}
