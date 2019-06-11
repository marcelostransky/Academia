using AcademiaDanca.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.AlunoComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.AlunoContexto
{
    public class EditarFotoAlunoManipulador : Notifiable, IComandoManipulador<EditarFotoAlunoComando>
    {
        private readonly IAlunoRepositorio _repositorio;
        public  EditarFotoAlunoManipulador(IAlunoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(EditarFotoAlunoComando comando)
        {
            // Validar Comando
            comando.Valido();
            AddNotifications(comando.Notifications);

            //Criar a entidade
            var aluno = new Aluno(comando.Id, comando.Foto);

            AddNotifications(aluno.Notifications);
            if (Invalid)
            {
                return new ComandoResultado(
                  false,
                  "Por favor, corrija os campos abaixo",
                  Notifications);
            }
            //Persistir os dados

            await _repositorio.EditarFotoAsync(aluno);

     


            // Retornar o resultado para tela
            return new ComandoResultado(true, "Funcionario cadastrado com sucesso", new
            {
                Id = 0,
                Nome = "",
                Status = true
            });
        }
    }
}
