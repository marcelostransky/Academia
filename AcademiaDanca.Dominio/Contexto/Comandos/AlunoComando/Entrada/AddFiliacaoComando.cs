using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.AlunoComando.Entrada
{
    public class AddFiliacaoComando : Notifiable, IComando
    {
        public int Id { get; set; }
        public int IdAluno { get; set; }
        public int TipoFiliacao { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }


        public bool Valido()
        {
            AddNotifications(new ValidationContract()
               .HasMinLen(Nome, 3, "Nome", "O nome deve conter pelo menos 3 caracteres.")
               .HasMaxLen(Nome, 300, "Nome", "O nome deve conter no máximo 300 caracteres.")
              
               .IsTrue(Id == 0, "Id", "Id informado não é valido.")
               .IsTrue(IdAluno >= 0, "IdAluno", "Aluno não informado.")
               .IsTrue(TipoFiliacao >= 0, "Tipo Filiação", "Tipo Filiação não informado.")
           );
            return Valid;
        }
    }
}
