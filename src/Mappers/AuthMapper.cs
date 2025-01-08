using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catedra3Backend.src.Dto;
using catedra3Backend.src.Models;

namespace catedra3Backend.src.Mappers
{
    public static class AuthMapper
    {
        public static AppUser ToUserRegister(this loginRegisterDto registerDto)
        {
            return new AppUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
            };
        }

        public static AppUserDto ToUserDto(this AppUser user)
        {
            return new AppUserDto
            {
                Id = user.Id,
                Email = user.Email!,
            };
        }

        public static AppUser ToUser(this AppUserDto userDto)
        {
            return new AppUser
            {
                Id = userDto.Id,
                Email = userDto.Email,
            };
        }
    }
}