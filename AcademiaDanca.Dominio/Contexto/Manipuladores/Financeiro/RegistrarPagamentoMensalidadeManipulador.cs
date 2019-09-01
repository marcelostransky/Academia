using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Financeiro
{
    public class RegistrarPagamentoMensalidadeManipulador : Notifiable, IComandoManipulador<RegistrarPagamentoMensalidadeComando>
    {
        private readonly IFinanceiroRepositorio _repositorio;
        public RegistrarPagamentoMensalidadeManipulador(IFinanceiroRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(RegistrarPagamentoMensalidadeComando comando)
        {
          
            comando.Valido();
            if (Invalid)
            {
                return new ComandoResultado(false, "Por favor, corrija os campos abaixo", Notifications);
            }
            //Persistir Dados
            var id = await _repositorio.RegistrarPagamentoAsync(comando.IdMensalidade, comando.Pago, comando.Juros);
            // Retornar o resultado para tela
            return new ComandoResultado(true, "pagamento realizado com sucesso", new
            {
                Id = comando.IdMensalidade,
                Nome = "Atualizado",
                Status = true
            });
        }
    }
}
