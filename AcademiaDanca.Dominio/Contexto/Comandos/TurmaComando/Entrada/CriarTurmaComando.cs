using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.TurmaComando.Entrada
{
    public class CriarTurmaComando : Notifiable, IComando
    {

        public int Id { get; set; }
        public string CodTurma { get; set; }
        public string DesTurma { get; set; }
        public int IdProfessor { get; set; }
        public int TipoTurmaId { get; set; }
       
        public List<CriarAgendaTurmaInicialComando> Agendamentos { get; set; }
        public bool Valido()
        {
            AddNotifications(new ValidationContract()
               .HasMinLen(DesTurma, 3, "Descricao", "Descricao deve conter pelo menos 3 caracteres")
               .HasMaxLen(DesTurma, 300, "login", "Descricao deve conter no máximo 300 caracteres")
               .IsTrue(Id == 0, "Id", "Id informado não é válido")
               .IsTrue(AgendamentoCompleto(), "Agendamento", "Agendamento sem sala informada")

           ); ;
            return Valid;
        }

        private bool AgendamentoCompleto()
        {
            foreach (var item in Agendamentos)
            {

            }
                return true;
        }
    }
}
