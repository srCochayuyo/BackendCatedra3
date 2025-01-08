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
        public string titulo {get; set;} = string.Empty!;

        [Required]
        public DateTime fechaPost {get; set;}

        [Required]
        public string image {get; set;} = string.Empty!;

        [Required]
        public string userId {get; set;} = string.Empty!;


    }
}