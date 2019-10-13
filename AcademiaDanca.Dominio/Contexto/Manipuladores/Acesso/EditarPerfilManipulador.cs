using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.AcessoComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Acesso
{
    public class EditarPerfilManipulador : Notifiable, IComandoManipulador<EditarPerfilComando>
    {
        private readonly IAcessoRepositorio _repositorio;
        public EditarPerfilManipulador(IAcessoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(EditarPerfilComando comando)
        {
            //Criar Entidade
            var perfil = new Perfil(comando.Id, comando.DesPerfil);
            //Validar Comando
            comando.Valido();
            AddNotifications(comando.Notifications);

            if (comando.Notifications.Count > 0 && await _repositorio.CheckPerfilAsync(comando.DesPerfil))
            {
                AddNotification("Perfil", "A descrição informada já está em uso");
            }
            if (Invalid)
            {
                return new ComandoResultado(
                  false,
                  "Por favor, corrija os campos abaixo",
                  Notifications);
            }
            //Persistir Dados
            var atualizado = await _repositorio.EditarPerfilAsync(perfil);

            // Retornar o resultado para tela
            return new ComandoResultado(true, "Dados de acesso atualizado com sucesso", new
            {
                Id = atualizado,
                Nome = "",
                Status = true
            });


        }
    }
}
