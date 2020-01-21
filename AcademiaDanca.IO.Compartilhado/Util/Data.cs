using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Compartilhado.Util
{
   public static class Data
    {
        public static string ObterDataFormatoUnix(DateTime data)
        {
            var d = data.ToShortDateString().Split('/');
            return $"{d[2]}-{d[1]}-{d[0]}";
        }
    }
}
