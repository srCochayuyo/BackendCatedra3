using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using catedra3Backend.src.Dto;
using catedra3Backend.src.Interface;
using catedra3Backend.src.Mappers;
using catedra3Backend.src.Models;
using catedra3Backend.src.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace catedra3Backend.src.controller
{
   [Route("api/auth")]
   [ApiController]
   
    public class AuthController : ControllerBase
    {

        private readonly IAuthRepository _authRepository;

        private readonly ITokenService _tokenService;

        public AuthController(IAuthRepository authRepository, ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
        }

        //Metodo para registrar usuarip
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] loginRegisterDto request)
        {
            try {


                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);

                }


                AppUser appUser = request.ToUserRegister();

                if(string.IsNullOrEmpty(request.Email))
                {
                    return BadRequest(new {message ="Error. El Email es requerido"});
                }


                if(string.IsNullOrEmpty(request.Password))
                {
                    return BadRequest(new { message = "Error. La contraseña es requerida"});
                    
                }

                var createUser = await _authRepository.CreateUserAsync(appUser, request.Password);

                if(createUser.Succeeded)
                {
                    var role = await _authRepository.AddRole(appUser,"User");

                    if(role.Succeeded)
                    {
                        return Ok(new{message = "Usuario registrado con exito"});

                    }else {

                        return StatusCode(500, new {message = role.Errors.Select(e => e.Description)});
                    }

                }else {

                    var errors = createUser.Errors.Select(e => e.Description);

                    if(errors.Any(e => e.Contains("Username")))
                    {

                        errors = errors.Select(e => e.Replace("Username", " Error. El correo electrónico ingresado ya existe en el sistema")).ToList();

                        errors = errors.Select(e => e.Replace(" is already taken", "")).ToList();
                        
                        errors = errors.Select(e => Regex.Replace(e, @"'[^']+'", "")).ToList();

                        return BadRequest(new {message = errors});
                    }

                    return StatusCode(500, new { message = errors});
                }


            }catch(Exception e) {

                 return StatusCode(500, new {message = e.Message});

            }
        }


        //Metodo para logearse
    [HttpPost("login")]
    public async Task<IActionResult> Login(loginRegisterDto request)
    {
        try {

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var result = await _authRepository.checkPasswordbyEmail(request.Email, request.Password);

            var AppUserDto = await _authRepository.GetUserByEmail(request.Email);

            AppUser appUser = AppUserDto.ToUser();

            if(!result.Succeeded || appUser == null)
            {
                return BadRequest( new {message = "Error. Usuario no registrado o contraseña incorrecta"});
            }

            string token = _tokenService.CreateToken(appUser);
        
            var response = new
            {

                message = "Incio de sesion exitoso",
                Token = token

            };

            return Ok(response);



            

        }catch(Exception e)
        {
            return StatusCode(500, new {message = e.Message});
        }
    }










        
    }

    



    
}