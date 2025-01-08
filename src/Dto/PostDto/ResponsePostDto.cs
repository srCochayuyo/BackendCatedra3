using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace catedra3Backend.src.Dto.PostDto
{
    public class ResponsePostDto
    {
        public string Titulo { get; set; } = string.Empty!;
        public DateTime FechaPost { get; set; }
        public string Image { get; set; } = string.Empty!;
    }
}