using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catedra3Backend.src.Data;
using catedra3Backend.src.Dto.PostDto;
using catedra3Backend.src.Interface;
using catedra3Backend.src.Mappers;
using catedra3Backend.src.Models;
using Microsoft.EntityFrameworkCore;

namespace catedra3Backend.src.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDBContext _context;

        public PostRepository(ApplicationDBContext context)
        {
            _context = context;
        }  

        public async Task<ResponsePostDto> CreatePost(CreatePostDto request,string imageUrl,string userId)
        {
            var PostRequest = new Post {
                Titulo = request.Titulo,
                //Hora chilena
                FechaPost = TimeZoneInfo.ConvertTime(DateTime.Now,TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time")),
                Image = imageUrl,
                UserID = userId
            };

            await _context.Post.AddAsync(PostRequest);
            await _context.SaveChangesAsync();

            var response = PostRequest.ToPostResponseDto();
            return response;
        }

        public async Task<List<ResponsePostDto>> getPosts()
        {
            var postList = await _context.Post.ToListAsync();

            if (!postList.Any()) 
            {
                throw new Exception("No Hay Post");
            }


            return postList.Select(c => c.ToPostResponseDto()).ToList();
        }
    }
}