using AcademiaDanca.IO.Compartilhado.Comando;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Compartilhado
{
    public class ComandoResultado : IComandoResultado
    {

        public ComandoResultado(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
        public ComandoResultado(bool success, string message, List<object> datas)
        {
            Success = success;
            Message = message;
            Data = datas;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public List<object> Datas { get; set; }

       
    }
}
