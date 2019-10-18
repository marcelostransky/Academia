using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Entidade
{
    public class Permissao
    {
        public int PaginaId { get;private set; }
        public int PerfilId { get; private set; }
        public bool Criar { get; private set; }
        public bool Ler { get; private set; }
        public bool Editar { get; private set; }
        public bool Excluir { get; private set; }
        public Permissao(
            int paginaId,
            int perfilId,
            bool criar,
            bool editar,
            bool ler,
            bool excluir
            )
        {
            PaginaId = paginaId;
            PerfilId = perfilId;
            Criar = criar;
            Editar = editar;
            Ler = ler;
            Excluir = excluir;
        }

      
    }
}
