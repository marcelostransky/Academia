using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.App.Models
{
    public class FotoModel
    {
        public int Id { get; set; }
        public string Foto { get; set; }
        public IFormFile file { get; set; }
        public string base64image { get; set; }
    }
}
