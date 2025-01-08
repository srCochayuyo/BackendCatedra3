using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catedra3Backend.src.Data;
using catedra3Backend.src.Interface;
using catedra3Backend.src.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace catedra3Backend.src.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthRepository(ApplicationDBContext context, UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult>CreateUserAsync(AppUser user, string password)
        {
            return await _userManager.CreateAsync(user,password);
        }

        public async Task<IdentityResult> AddRole(AppUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> checkPasswordbyEmail(string email, string Password)
        {
            var appUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if(appUser == null)
            {
                throw new Exception("Error. Usuario no registrado o contraseña incorrecta");
            }


            var checkPassword =  await _signInManager.CheckPasswordSignInAsync(appUser!,Password,false);

            if(checkPassword.Succeeded)
            {
                return IdentityResult.Success;
            }

            throw new Exception("Error. Usuario no registrado o contraseña incorrecta");
        }
    }
}