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
    public class EditarPaginaManipulador : Notifiable, IComandoManipulador<EditarPaginaComando>
    {

        private readonly IAcessoRepositorio _repositorio;
        public EditarPaginaManipulador(IAcessoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(EditarPaginaComando comando)
        {
            //Validar Comando
            comando.Valido();
            //Criar Entidade
            var pagina = new Pagina(comando.Id, comando.DesPagina, comando.Chave);

            //Validar pagina unica
            if (comando.Notifications.Count <= 0 && await _repositorio.CheckPaginaAsync(comando.DesPagina, comando.Chave))
            {
                AddNotification("Descrição", "A descrição informada já está em uso");
            }
            AddNotifications(comando.Notifications);

            if (Invalid)
            {
                return new ComandoResultado(
                  false,
                  "Por favor, corrija os campos abaixo",
                  Notifications);
            }
            //Persistir Dados
            int atualizado = await _repositorio.EditaPaginaAsync(pagina);

            // Retornar o resultado para tela
            return new ComandoResultado(true, "Página atualizada com sucesso", new
            {
                Id = atualizado,
                Nome = "",
                Status = true
            });

        }
    }
}
