using AcademiaDanca.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.TurmaComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.TurmaContexto
{
    public class CriarTurmaManipulador : Notifiable, IComandoManipulador<CriarTurmaComando>
    {
        private readonly ITurmaRepositorio _repositorio;
        public CriarTurmaManipulador(ITurmaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(CriarTurmaComando comando)
        {
            
            //Criar Entidades
            var tipoTurma = new TurmaTipo(comando.TipoTurmaId);
            var funcionario = new Funcionario(comando.IdProfessor);
            var turma = new Turma(comando.Id, comando.CodTurma, comando.DesTurma, funcionario, tipoTurma, comando.Ano);
            
            //verificar turma existente
            if (await _repositorio.CheckTurmaAsync(turma))
                AddNotification("Turma", "A turma informada já está em uso");

            //Validar Comando
            comando.Valido();

            AddNotifications(funcionario.Notifications);

            if (Invalid)
            {
                return new ComandoResultado(
                  false,
                  "Por favor, corrija os campos abaixo",
                  Notifications);
            }
            //Persistir os dados

            await _repositorio.SalvarAsync(turma);
            // Retornar o resultado para tela
            return new ComandoResultado(true, "Turma cadastrada com sucesso", new
            {
                Id = 0,
                Nome = "",
                Status = true
            });
        }
    }
}
