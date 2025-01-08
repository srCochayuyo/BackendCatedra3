using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace catedra3Backend.src.Models
{
    public class Post
    {
        public int ID {get; set;}

        public string Titulo {get; set;} = string.Empty;

        public string Image {get; set;} =string.Empty;

        [ForeignKey("AppUser")]
        public string UserID {get; set;} = string.Empty;
    }
}