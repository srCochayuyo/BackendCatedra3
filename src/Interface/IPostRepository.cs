using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catedra3Backend.src.Dto.PostDto;
using catedra3Backend.src.Models;

namespace catedra3Backend.src.Interface
{
    public interface IPostRepository
    {
        Task<ResponsePostDto> CreatePost(CreatePostDto request,string imageUrl,string userId);
        
    }
}