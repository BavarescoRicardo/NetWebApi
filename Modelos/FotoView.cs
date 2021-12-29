using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetWebApi.Modelos
{
    public class FotoView
    {
        public string NomeImagem { get; set; }

        public int refUsuario { get; set; }

        public string regerenciaadd { get; set; }

        public IFormFile Foto { get; set; }
    }
}
