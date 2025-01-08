using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catedra3Backend.src.Dto.PostDto;
using catedra3Backend.src.Models;

namespace catedra3Backend.src.Mappers
{
    public static class PostMappers
    {
        public static ResponsePostDto ToPostResponseDto(this Post post)
        {
            return new ResponsePostDto
            {
            Titulo = post.Titulo,
            FechaPost = post.FechaPost,
            Image = post.Image
            };
        }
    }
}