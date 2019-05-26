using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FuncionarioComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using AcademiaDanca.IO.Dominio.Contexto.Vo;
using FluentValidator;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores
{
    public class EditarFotoFuncionarioManipulador : Notifiable, IComandoManipulador<EditarFotoFuncionarioComando>
    {
        private readonly IFuncionarioRepositorio _repositorio;
        public EditarFotoFuncionarioManipulador(IFuncionarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(EditarFotoFuncionarioComando comando)
        {

          
            // Validar Comando
            comando.Valido();
            AddNotifications(comando.Notifications);
          
            //Criar a entidade
            var funcionario = new Funcionario(comando.Id, comando.Foto);

            AddNotifications(funcionario.Notifications);
            if (Invalid)
            {
                return new ComandoResultado(
                  false,
                  "Por favor, corrija os campos abaixo",
                  Notifications);
            }
            //Persistir os dados

             await _repositorio.EditarFotoAsync(funcionario);

            //Se for usuario diferente de professor validar se tem q desalocar turma


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
