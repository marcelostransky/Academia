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
    public class AgendarTurmaManipulador : Notifiable, IComandoManipulador<CriarAgendaTurmaComando>
    {
        private readonly IAgendaRepositorio _repositorio;
        public AgendarTurmaManipulador(IAgendaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(CriarAgendaTurmaComando comando)
        {
            //Criar Entidades
            
            var sala = new Sala(comando.IdSala);
            var diaSemana = new DiaSemana(comando.IdDiaSemana);
            var turma = new Turma(comando.IdTurma);
            var agenda = new Agenda(comando.Id, comando.Hora, turma, diaSemana, sala);
            //verificar turma existente
            if (await _repositorio.CheckAgendamentoAsync(agenda))
                AddNotification("Turma", "Já existe um agendamento cadastrado para os dados informado");

            //Validar Comando
            comando.Valido();

            AddNotifications(diaSemana.Notifications);
            AddNotifications(turma.Notifications);
            AddNotifications(agenda.Notifications);

            if (Invalid)
            {
                return new ComandoResultado(
                  false,
                  "Por favor, corrija os campos abaixo",
                  Notifications);
            }
            //Persistir os dados

            await _repositorio.SalvarAsync(agenda);
            // Retornar o resultado para tela
            return new ComandoResultado(true, "Turma agendada com sucesso", new
            {
                Id = 0,
                Nome = "",
                Status = true
            });
        }
    }
}
