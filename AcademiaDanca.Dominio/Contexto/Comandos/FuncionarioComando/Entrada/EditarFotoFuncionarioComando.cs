using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.FuncionarioComando.Entrada
{
    public class EditarFotoFuncionarioComando : Notifiable, IComando
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
