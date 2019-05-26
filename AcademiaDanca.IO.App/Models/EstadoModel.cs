using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.App.Models
{
    public class EstadoModel
    {
        public string Uf { get; set; }

        public async Task<string[]> ObterListaUF()
        {
            string[] estados =
               {
                    "AC",
                    "AL",
                    "AM",
                    "AP",
                    "BA",
                    "CE",
                    "DF",
                    "ES",
                    "GO",
                    "MA",
                    "MG",
                    "MS",
                    "MT",
                    "PA",
                    "PB",
                    "PE",
                    "PI",
                    "PR",
                    "RJ",
                    "RN",
                    "RO",
                    "RR",
                    "RS",
                    "SC",
                    "SE",
                    "SP",
                    "TO"
                };
            return await Task.Run(() => estados);
        }
        
    }
}
