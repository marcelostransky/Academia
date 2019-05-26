using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.TipoFiliacaoComando.Entrada
{
    public class CriarTipoFiliacaoComando : Notifiable, IComando
    {
        public string DesTipoFiliacao { get; set; }
        public bool Valido()
        {
            AddNotifications(new ValidationContract()
                .HasMinLen(DesTipoFiliacao, 3, "Tipo de filiação", "O tipo filiação deve conter pelo menos 3 caracteres")
                .HasMaxLen(DesTipoFiliacao, 45, "Tipo de filiação", "O tipo filiação deve conter no máximo 45 caracteres")

                );
            return Valid;
        }
    }
}
