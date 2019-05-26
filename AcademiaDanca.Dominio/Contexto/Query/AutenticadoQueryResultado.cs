using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Query
{
    public class AutenticarFuncionarioQueryResultado
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public string Email { get; set; }
        public string Perfil { get; set; }
    }
}
