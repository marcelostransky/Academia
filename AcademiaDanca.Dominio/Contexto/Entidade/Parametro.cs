using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Entidade
{
   public class Parametro
    {
        public int Id { get; set; }
        public string Chave { get; set; }
        public string Valor { get; set; }

        public Parametro(int id, string chave, string valor)
        {
            Id = id;
            Chave = chave;
            Valor = valor;
        }
    }
}
