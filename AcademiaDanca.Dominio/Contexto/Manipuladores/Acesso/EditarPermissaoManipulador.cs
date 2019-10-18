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
  public  class EditarPermissaoManipulador : Notifiable, IComandoManipulador<EditarPermissaoComando>
    {
        private readonly IAcessoRepositorio _repositorio;
        public EditarPermissaoManipulador(IAcessoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(EditarPermissaoComando comando)
        {
            //Obter id pagina pela constante
            comando.PaginaId = await _repositorio.ObterIdPorConstante(comando.Constante);


            //Validar Comando
            comando.Valido();

            //Criar Entidade

            var permissao = new Permissao(comando.PaginaId, comando.PerfilId, comando.Criar, comando.Editar, comando.Ler, comando.Excluir);

            AddNotifications(comando.Notifications);
            if (Invalid)
            {
                return new ComandoResultado(false, "Por favor, corrija os campos abaixo", Notifications);
            }
            //Persistir Dados
            var total = await _repositorio.EditaPermissaoAsync(permissao);
            // Retornar o resultado para tela
            return new ComandoResultado(true, "Permissão atulizada com sucesso", new
            {
                Id = total,
                Nome = "OK",
                Status = true
            });
        }
    }
}
