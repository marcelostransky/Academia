using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Query
{
    public class FuncioanrioQueryPorNomeResultado
    {
        public int IdUsuario { get; set; }
        public string NomeFuncionario { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Foto { get; set; }
        public int IdPerfil { get; set; }
        public string Perfil { get; set; }
        public bool Status { get; set; }
    }
}
