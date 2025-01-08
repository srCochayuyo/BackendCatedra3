using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catedra3Backend.src.Dto;
using catedra3Backend.src.Models;
using Microsoft.AspNetCore.Identity;

namespace catedra3Backend.src.Interface
{
    public interface IAuthRepository
    {
        Task<IdentityResult> CreateUserAsync(AppUser user, string password);

        Task<IdentityResult> AddRole(AppUser user, string role);

        Task<IdentityResult> checkPasswordbyEmail(string id, string newPassword);

         Task<string> GetTokenByEmail(string email);

    }
}