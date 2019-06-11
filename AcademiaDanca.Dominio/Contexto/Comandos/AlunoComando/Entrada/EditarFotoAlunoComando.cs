using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.AlunoComando.Entrada
{
    public class EditarFotoAlunoComando : Notifiable, IComando
    {
        public int Id { get; set; }
        public string Foto { get; set; }



        public bool Valido()
        {
            AddNotifications(new ValidationContract()
               .IsTrue(Id > 0, "Codigo Funcionario", "Codigo não informado")
               .IsNotNullOrEmpty(Foto, "Foto", "Foto não informada")
           );
            return Valid;
        }
    }
}
