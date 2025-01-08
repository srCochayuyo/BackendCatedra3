using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace catedra3Backend.src.Dto.PostDto
{
    public class CreatePostDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "El título debe tener al menos 5 caracteres.")]
        public string Titulo {get; set;} = string.Empty!;

        [Required]
        public DateTime FechaPost {get; set;}

        [Required]
        public string Image {get; set;} = string.Empty!;

        [Required]
        public string userId {get; set;} = string.Empty!;


    }
}