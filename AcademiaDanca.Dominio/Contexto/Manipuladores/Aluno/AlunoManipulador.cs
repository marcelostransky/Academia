using AcademiaDanca.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.Aluno.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Aluno
{
    public class AlunoManipulador : Notifiable, IComandoManipulador<CriarAlunoComando>
    {
        private readonly IAlunoRepositorio _repositorio;
        public AlunoManipulador(IAlunoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(CriarAlunoComando comando)
        {

            var email = new Vo.Email(comando.Email);
            ////Ckeck Email
            //if (!string.IsNullOrEmpty(comando.Email) && (await _repositorio.CheckEmailAsync(comando.Email)))
            //    AddNotification("Email", "Email informado já está em uso no sistema");
            //else
            //    AddNotifications(new Vo.Email(comando.Email).Notifications);

            ////Check Cpf
            //if (!string.IsNullOrEmpty(comando.Cpf) && (await _repositorio.CheckCpfAsync(comando.Cpf)))
            //    AddNotification("CPF", "Cpf informado não é válido");
            //else
            //    AddNotifications(new Vo.Email(comando.Email).Notifications);

            //Criar Entidade
            var aluno = new AcademiaDanca.Dominio.Contexto.Entidade.Aluno(comando.Id, comando.Nome, comando.DataNascimento, null, email, comando.UifId, comando.Telefone, comando.Celular, comando.Foto, null);

            //Validar Comando
            comando.Valido();
            if (Invalid)
            {
                return new ComandoResultado(false, "Por favor, corrija os campos abaixo", Notifications);
            }
            //Persistir Dados
            var id = await _repositorio.SalvarAsync(aluno);
            // Retornar o resultado para tela
            return new ComandoResultado(true, "Aluno cadastrado com sucesso", new
            {
                Id = id,
                Nome = aluno.Nome,
                Status = true
            });
        }
    }
}
