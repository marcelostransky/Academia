using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FuncionarioComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores
{
    public class EditarAcessoFuncionarioManipulador : Notifiable, IComandoManipulador<EditarAcessoFuncionarioComando>
    {
        private readonly IFuncionarioRepositorio _repositorio;
        public EditarAcessoFuncionarioManipulador(IFuncionarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(EditarAcessoFuncionarioComando comando)
        {
            //Validar Comando
            comando.Valido();

            AddNotifications(comando.Notifications);

            if (Invalid)
            {
                return new ComandoResultado(
                  false,
                  "Por favor, corrija os campos abaixo",
                  Notifications);
            }
            //Persistir Dados
            var atualizado = await _repositorio.EditarAcessoAsync(comando.Id, comando.Senha);

            // Retornar o resultado para tela
            return new ComandoResultado(true, "Dados de acesso atualizado com sucesso", new
            {
                Id = 0,
                Nome = "",
                Status = true
            });


        }
    }
}
