using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Compartilhado
{
    public static class ChaveRegistro
    {
        public static Guid Gerar()
        {
            return Guid.NewGuid();
        }
    }
}
