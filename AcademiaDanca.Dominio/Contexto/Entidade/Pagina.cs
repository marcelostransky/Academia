using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Entidade
{
    public class Pagina : Notifiable
    {
        public Pagina(int id, string desPagina)
        {
            Id = id;
            DesPagina = desPagina;
        }

        public int Id { get; private set; }
        public string DesPagina { get; private set; }
    }
}
