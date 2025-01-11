using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using catedra3Backend.src.Dto.PostDto;
using catedra3Backend.src.Interface;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace catedra3Backend.src.controller
{
    [Route("api/posts")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        private readonly Cloudinary _cloudinary;


        public PostController(IPostRepository postRepository, Cloudinary cloudinary)
        {
            _postRepository = postRepository;
            _cloudinary = cloudinary;
        }


        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> createPost([FromForm] CreatePostDto request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(request.Image == null || request.Image.Length == 0)
            {
                return BadRequest(new {message = "La imagen es requerida para el post"});
            }

            if(request.Image.ContentType != "image/jpeg" && request.Image.ContentType != "image/png")
            {
                return BadRequest(new {message = "La imagen debe ser jpeg o png"});
            }

            if(request.Image.Length > 5 *1024*1024)
            {
                return BadRequest(new {message = "La imagen debe pesar menos de 5mb"});
            }

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(request.Image.FileName, request.Image.OpenReadStream()),
                Folder = "Catedra3"
                
            };

            var uploadResults = await _cloudinary.UploadAsync(uploadParams);

            if(uploadResults.Error != null)
            {
                return BadRequest(new {message = uploadResults.Error.Message});
            }

            //obtenemos url de la imagen
            string urlImage = uploadResults.SecureUrl.AbsoluteUri;

            //obtenemos del usuario
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)!;
            var userId = userIdClaim.Value;

            var result = await _postRepository.CreatePost(request,urlImage,userId);


            return Ok(new {
                message = "Post creado con exito",
                Post = result

            });


        }


        [HttpGet]
        public async Task<IActionResult> getAllPost()
        {
            try{

                var posts = await _postRepository.getPosts();

                return Ok(new {
                    messgae = "Posts obtenidos con exito",
                    Posts = posts
                });

            }catch(Exception e){

                return BadRequest(new {message =e.Message});
            }
        }
    }
}