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
    public class DelPaginaManipulador : Notifiable, IComandoManipulador<DelPaginaComando>
    {
        private readonly IAcessoRepositorio _repositorio;
        public DelPaginaManipulador(IAcessoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(DelPaginaComando comando)
        {
            //Validar se pode excluir a pagina
            if (await _repositorio.CheckPermissaoAsync(comando.PaginaId, 0))
                AddNotification("Pagina", $"Pagina informada possui regras de permissao vinculada. " +
                    $" Exclua as permissoes e tente novamente");


            //Validar Comando
            comando.Valido();
            AddNotifications(comando.Notifications);
            if (Invalid)
            {
                return new ComandoResultado(false, "Por favor, corrija os campos abaixo", Notifications);
            }
            //Persistir Dados
            if (!await _repositorio.DeletarPaginaAsync(comando.PaginaId))
            {
                throw new Exception("Sistema não conseguiu processar esta solicitação");
            }
            // Retornar o resultado para tela
            return new ComandoResultado(true, "Página excluida com sucesso.", new
            {
                Id = 0,
                Nome = "OK",
                Status = true
            });
        }
    }
}
