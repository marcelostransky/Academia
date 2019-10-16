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

    public class DelPerfilManipulador : Notifiable, IComandoManipulador<DelPerfilComando>
    {
        private readonly IAcessoRepositorio _repositorio;
        public DelPerfilManipulador(IAcessoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(DelPerfilComando comando)
        {
            //Validar se pode excluir o perfil
            if (await _repositorio.CheckPermissaoAsync(0, comando.Id))
                AddNotification("Pagina", $"Perfil informada possui regras de permissao vinculada. " +
                    $" Exclua as permissoes e tente novamente");
            //Validar se existe usuario vinculado ao perfil
            if (await _repositorio.CheckFuncionarioPerfilAsync(comando.Id))
                AddNotification("Pagina", $"Perfil informada esta atrelado  em usuario. " +
                    $" Exclua os vinculos e tente novamente");

            //Validar Comando
            comando.Valido();
            AddNotifications(comando.Notifications);
            if (Invalid)
            {
                return new ComandoResultado(false, "Por favor, corrija os campos abaixo", Notifications);
            }
            //Persistir Dados
            if (!await _repositorio.DeletarPaginaAsync(comando.Id))
            {
                throw new Exception("Sistema não conseguiu processar esta solicitação");
            }
            // Retornar o resultado para tela
            return new ComandoResultado(true, "Pefil excluida com sucesso.", new
            {
                Id = 0,
                Nome = "OK",
                Status = true
            });
        }
    }
}
