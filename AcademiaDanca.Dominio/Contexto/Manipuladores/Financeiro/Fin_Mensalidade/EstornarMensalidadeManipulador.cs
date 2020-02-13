using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada.Com_Mensalidade;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Financeiro.Fin_Mensalidade
{
    public class EstornarMensalidadeManipulador : Notifiable, IComandoManipulador<EstornarMensalidadeComando>
    {
        private readonly IMensalidadeRepositorio _repositorio;
        public EstornarMensalidadeManipulador(IMensalidadeRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(EstornarMensalidadeComando comando)
        {
            comando.Valido();
            if (Invalid)
            {
                return new ComandoResultado(false, "Por favor, corrija os campos abaixo", Notifications);
            }
            //Persistir Dados
            var id = await _repositorio.EstornarAsync(comando.IdMensalidade,comando.IdUsuario);
            
            
            // Retornar o resultado para tela
            return new ComandoResultado(true, "Estornado realizado com sucesso", new
            {
                Id = comando.IdMensalidade,
                Nome = "Estorno",
                Status = true
            });
        }
    }
}
