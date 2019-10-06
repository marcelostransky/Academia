using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Query.Acesso
{
   public class PermissaoResultadoQuery
    {
       
        public string PaginaId { get; set; }
        public int PapelId { get; set; }
        public string DesPapel { get; set; }
        public string DesPagina { get; set; }
        public bool Criar { get; set; }
        public bool Editar { get; set; }
        public bool Excluir { get; set; }
        public bool Ler { get; set; }
    }
}
