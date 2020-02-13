using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.ConfiguracaoComando.Entrada
{
    public class ParametroComando : Notifiable, IComando
    {
        public List<ParametroItem> Parametros { get; set; }

        public bool Valido()
        {
            AddNotifications(new ValidationContract()
                 .IsTrue(Parametros.Count > 0, "Chave/Valor", "ChaveValor não localizado")
          );
            return Valid;
        }
    }
    public class ParametroItem
    {
        public string Chave { get; set; }
        public string Valor { get; set; }
        public string ValorAtual { get; set; }
    }
}
