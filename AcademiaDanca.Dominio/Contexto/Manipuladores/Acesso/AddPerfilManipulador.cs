using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.Acesso.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Acesso
{
    public class AddPerfilManipulador : Notifiable, IComandoManipulador<AddPerfilComando>
    {
        private readonly IAcessoRepositorio _repositorio;
        public AddPerfilManipulador(IAcessoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(AddPerfilComando comando)
        {
            //Criar Entidade
            var perfil = new Perfil(comando.Id, comando.DesPerfil);

            //Validar Turma/Aluno Unico
            if (await _repositorio.CheckPerfilAsync(perfil.DesPerfil))
                AddNotification("Descricao", "Perfil já cadastrada no sistema");

            //Validar Comando
            comando.Valido();
            if (Invalid)
            {
                return new ComandoResultado(false, "Por favor, corrija os campos abaixo", Notifications);
            }
            //Persistir Dados
            var id = await _repositorio.SalvarPerfilAsync(perfil);
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
