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
    public class AddPermissaoManipulador : Notifiable, IComandoManipulador<AddPermissaoComando>
    {
        private readonly IAcessoRepositorio _repositorio;
        public AddPermissaoManipulador(IAcessoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IComandoResultado> ManipuladorAsync(AddPermissaoComando comando)
        {
            //Criar Entidade
            var permissao = new Permissao(comando.PaginaId, comando.PerfilId, comando.Criar, comando.Editar, comando.Ler, comando.Excluir);


            //Validar Turma/Aluno Unico
            if (await _repositorio.CheckPermissaoAsync(permissao.PaginaId, permissao.PerfilId))
                AddNotification("Permissão", "Permissão já cadastrado no sistema");

            //Validar Comando
            comando.Valido();
            if (Invalid)
            {
                return new ComandoResultado(false, "Por favor, corrija os campos abaixo", Notifications);
            }
            //Persistir Dados
            var id = await _repositorio.SalvarPermissaoAsync(permissao);
            // Retornar o resultado para tela
            return new ComandoResultado(true, "Perfil cadastrado com sucesso", new
            {
                Id = id,
                Nome = "OK",
                Status = true
            });
        }
    }
}
