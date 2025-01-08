using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catedra3Backend.src.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace catedra3Backend.src.Data
{
    public class DataSeeder
    {
         public static async Task SeedData(UserManager<AppUser> userManager, ApplicationDBContext context)
        {
            // Verificar si ya existen usuarios
            if (!userManager.Users.Any())
            {
                // Crear lista de usuarios
                var users = new List<AppUser>
                {
                    new AppUser { UserName = "leomessi@gmail.com", Email = "leomessi@gmail.com" },
                    new AppUser { UserName = "barcelona@gmail.com", Email = "barcelona@gmail.com" },
                    new AppUser { UserName = "cochayuyo@gmail.com", Email = "cochayuyo@gmail.com" }
                };

                // Crear usuarios con contraseña
                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "contrasena123");
                }

                await context.SaveChangesAsync();
            }

            // Verificar si ya existen posts
            if (!context.Post.Any())
            {
                var users = await userManager.Users.ToListAsync();
                
                Random random = new Random();

                var posts = new List<Post>
                {
                    new Post
                    {
                        Titulo = "Impossibles is nothing",
                        FechaPost = new DateTime(2022, 12, 18)
                            .AddHours(random.Next(0, 24))  
                            .AddMinutes(random.Next(0, 60))  
                            .AddSeconds(random.Next(0, 60)),  
                        Image = "https://res.cloudinary.com/dms2bhrnq/image/upload/v1736356882/impossibleisnothing_pcprqw.jpg",
                        UserID = users[0].Id
                    },
                    new Post
                    {
                        Titulo = "What a night, culers",
                        FechaPost = new DateTime(2024, 10, 26)
                            .AddHours(random.Next(0, 24))
                            .AddMinutes(random.Next(0, 60))
                            .AddSeconds(random.Next(0, 60)),
                        Image = "https://res.cloudinary.com/dms2bhrnq/image/upload/v1736356981/04_u10lsa.jpg",
                        UserID = users[1].Id
                    },
                    new Post
                    {
                        Titulo = "Messi doing Messi things",
                        FechaPost = new DateTime(2024, 12, 14)
                            .AddHours(random.Next(0, 24))
                            .AddMinutes(random.Next(0, 60))
                            .AddSeconds(random.Next(0, 60)),
                        Image = "https://res.cloudinary.com/dms2bhrnq/image/upload/v1736356660/messi_it0f6o.jpg",
                        UserID = users[1].Id
                    },
                    new Post
                    {
                        Titulo = "Pasar es pasar",
                        FechaPost = new DateTime(2025, 01, 03)
                            .AddHours(random.Next(0, 24))
                            .AddMinutes(random.Next(0, 60))
                            .AddSeconds(random.Next(0, 60)),
                        Image = "https://res.cloudinary.com/dms2bhrnq/image/upload/v1736357321/WhatsApp_Image_2025-01-08_at_14.27.36_kygsac.jpg",
                        UserID = users[2].Id
                    },
                    new Post
                    {
                        Titulo = "Just Tayler the crator",
                        FechaPost = new DateTime(2025, 01, 06)
                            .AddHours(random.Next(0, 24))
                            .AddMinutes(random.Next(0, 60))
                            .AddSeconds(random.Next(0, 60)),
                        Image = "https://res.cloudinary.com/dms2bhrnq/image/upload/v1736357600/WhatsApp_Image_2025-01-08_at_14.32.48_ueo3oe.jpg",
                        UserID = users[2].Id
                    }
                };

                await context.Post.AddRangeAsync(posts);
                await context.SaveChangesAsync();
            }
        }
    }
}