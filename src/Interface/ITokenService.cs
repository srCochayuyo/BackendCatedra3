using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catedra3Backend.src.Models;

namespace catedra3Backend.src.Interface
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}