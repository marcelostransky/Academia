using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.TurmaComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Query.Turma;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.TurmaContexto
{
    public class DeletarTurmaManipulador : Notifiable, IComandoManipulador<DeletarTurmaComando>
    {
        private readonly ITurmaRepositorio _repositorio;
        public DeletarTurmaManipulador(ITurmaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(DeletarTurmaComando comando)
        {
            //Validar se turma pode ser deletada

            TurmaQuantitativoQueryResultado quantitativos = await _repositorio.CheckQuantitativoTurmaAsync(comando.Id);
            if(quantitativos.TotalAluno>0)
                AddNotification("Aluno","Turma possui aluno vinculado. ");
            if (quantitativos.TotalAgendamento > 0)
                AddNotification("Agenda", "Turma possui agendamento vinculado.");
            if (quantitativos.TotalFinanceiro > 0)
                AddNotification("Financeito", "Turma possui matricula vinculado.");
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
            //Persistir os dados

            await _repositorio.DeletarAsync(comando.Id);
            // Retornar o resultado para tela
            return new ComandoResultado(true, "Deletado com sucesso", new
            {
                Id = comando.Id,
                Nome = "",
                Status = true
            });
        }
    }
}
