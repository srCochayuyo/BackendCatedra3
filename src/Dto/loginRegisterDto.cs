using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace catedra3Backend.src.Dto
{
    public class loginRegisterDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Email {get; set;} = string.Empty!;

        [Required]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-zA-Z]).{6,}$", ErrorMessage = "La contraseña debe contener al menos un número y una letra.")]
        public string Password {get; set;} = string.Empty!;
    }
}