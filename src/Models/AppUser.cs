using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace catedra3Backend.src.Models
{
    public class AppUser : IdentityUser
    {
        public string Password {get; set;} = string.Empty;
    }
}