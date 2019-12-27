using AcademiaDanca.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.TurmaComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.TurmaContexto
{
    public class EditarTurmaManipulador : Notifiable, IComandoManipulador<EditarTurmaComando>
    {
        private readonly ITurmaRepositorio _repositorio;
        public EditarTurmaManipulador(ITurmaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IComandoResultado> ManipuladorAsync(EditarTurmaComando comando)
        {

            //Criar Entidades
            var tipoTurma = new TurmaTipo(comando.IdTipoTurma);
            var funcionario = new Funcionario(comando.IdProfessor);
            var turma = new Turma(comando.Id, comando.CodTurma, comando.DesTurma, funcionario, tipoTurma, comando.Status);

            if ( (comando.Valor == comando.ValorAtual && comando.Status == comando.StatusAtual && comando.IdProfessor == comando.IdProfessorAtual) && await _repositorio.CheckTurmaAsync(turma))
            {
                AddNotification("Turma", "Não foi detectado alteração no dados apresentado. Operação não realizada");
            }


            //Validar Comando
            comando.Valido();
            AddNotifications(comando.Notifications);
            AddNotifications(funcionario.Notifications);

            if (Invalid)
            {
                return new ComandoResultado(
                  false,
                  "Por favor, corrija os campos abaixo",
                  Notifications);
            }
            //Persistir os dados

            await _repositorio.EditarAsync(turma);
            // Retornar o resultado para tela
            return new ComandoResultado(true, "Turma atualizada com sucesso", new
            {
                Id = 0,
                Nome = "",
                Status = true
            });
        }
    }
}
