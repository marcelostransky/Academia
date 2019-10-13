using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.AcessoComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Acesso
{
    public class DelPermissaoManipulador : Notifiable, IComandoManipulador<DelPermissaoComando>
    {
        private readonly IAcessoRepositorio _repositorio;
        public DelPermissaoManipulador(IAcessoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(DelPermissaoComando comando)
        {

            //Validar Comando
            comando.Valido();
            if (Invalid)
            {
                return new ComandoResultado(false, "Por favor, corrija os campos abaixo", Notifications);
            }
            
            //Persistir Dados
            var total = await _repositorio.DeletarPermissaoAsync(comando.PerfilId, comando.PaginaId);
            
            // Retornar o resultado para tela
            return new ComandoResultado(true, "Permissão excluido com sucesso.", new
            {
                Id = total,
                Nome = "OK",
                Status = true
            });
        }
    }
}
