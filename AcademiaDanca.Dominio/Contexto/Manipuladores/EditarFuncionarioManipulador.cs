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
    public class EditarFuncionarioManipulador : Notifiable, IComandoManipulador<EditarBaseFuncionarioComando>
    {
        private readonly IFuncionarioRepositorio _repositorio;
        public EditarFuncionarioManipulador(IFuncionarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(EditarBaseFuncionarioComando comando)
        {

            //Validar Cpf Unico
            if (comando.Cpf != comando.CpfAtual && await _repositorio.CheckCpfAsync(comando.Cpf))
            {
                AddNotification("Cpf", "Cpf informado já está em uso no sistema");
            }
            //Validar Email Unico
            if (comando.Email != comando.EmailAtual && await _repositorio.CheckEmailAsync(comando.Email))
            {
                AddNotification("Email", "Email informado já está em uso no sistema");
            }
            //Se o perfil for  =  professor, deve validar se possui turmas ativa. Se sim não prosseguir com atualização
            if (comando.DescPerfilAtual != comando.DescPerfil && await _repositorio.CheckPerfilProfessorTurmaAtivaAsync(comando.Id))
            {
                AddNotification("Perfil", "Funcionario não pode ser atualizado. Exclua as turmas para este professor e tente novamente");
            }
            // Validar Comando
            comando.Valido();

            AddNotifications(comando.Notifications);
            // Validara Vo
            var email = new Email(comando.Email);
            var nome = new Nome(comando.Nome);
            var cpf = new Cpf(comando.Cpf);

            AddNotifications(email.Notifications);
            AddNotifications(nome.Notifications);
            AddNotifications(cpf.Notifications);
            
            //Criar a entidade
            var funcionario = new Funcionario(comando.Id, nome, email, cpf, comando.DataNascimento, comando.IdPerfil, null);

            AddNotifications(funcionario.Notifications);
            if (Invalid)
            {
                return new ComandoResultado(
                  false,
                  "Por favor, corrija os campos abaixo",
                  Notifications);
            }
            //Persistir os dados

            await _repositorio.EditarAsync(funcionario);

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