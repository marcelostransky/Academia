using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada;
using FluentValidator;
using System;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Financeiro
{
    public class CriarMensalidadeManipuladores : Notifiable, IComandoManipulador<CriarMensalidadeComando>
    {
     

        public Task<IComandoResultado> ManipuladorAsync(CriarMensalidadeComando comando)
        {
            throw new NotImplementedException();
        }
    }
}
