using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.TipoTelefoneComando.Entrada
{
    public class CreateTipoTelefoneComando : Notifiable, IComando
    {
        public string DesTipoTelefone { get; set; }
        public bool Valido()
        {
            AddNotifications(new ValidationContract()
                .HasMinLen(DesTipoTelefone, 3, "Tipo de telefone", "O tipo de telefone deve conter pelo menos 3 caracteres")
                .HasMaxLen(DesTipoTelefone, 45, "Tipo de telefone", "O tipo de telefone deve conter no máximo 45 caracteres")

                );
            return Valid;
        }
    }
}
