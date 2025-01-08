using System.Text;
using catedra3Backend.src.Data;
using catedra3Backend.src.Interface;
using catedra3Backend.src.Models;
using catedra3Backend.src.Repository;
using catedra3Backend.src.services;
using CloudinaryDotNet;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

//Cloudinary
string CloudName = Environment.GetEnvironmentVariable("Cloudinary_cloudname") ?? "cloud_name";
string CloudKey = Environment.GetEnvironmentVariable("Cloudinary_apiKey") ?? "cloud_Key";
string CloudASecret = Environment.GetEnvironmentVariable("Cloudinary_ApiSecret") ?? "cloud_secrete";

var cloudinaryAccount = new Account(
    CloudName,
    CloudKey,
    CloudASecret
);

var Cloudinary = new Cloudinary(cloudinaryAccount);
builder.Services.AddSingleton(Cloudinary);

//Identity Role
builder
    .Services.AddIdentity<AppUser, IdentityRole>(opt =>
    {
        opt.Password.RequireDigit = true;
        opt.Password.RequireLowercase = false;
        opt.Password.RequireUppercase = false;
        opt.Password.RequireNonAlphanumeric = false;
        opt.Password.RequiredLength = 6;
    })
    .AddEntityFrameworkStores<ApplicationDBContext>();

//Authentication
builder
    .Services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme =
            opt.DefaultChallengeScheme =
            opt.DefaultForbidScheme =
            opt.DefaultScheme =
            opt.DefaultSignInScheme =
            opt.DefaultSignOutScheme =
                JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
            ValidateAudience = true,
            ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    Environment.GetEnvironmentVariable("JWT_KEY")
                        ?? throw new ArgumentNullException("JWT: SigningKey")
                )
            ),
        };
    });

//SwaggerGen
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer",
        }
    );
    option.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",
                    },
                },
                new string[] { }
            },
        }
    );
});    

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

//repositorios
builder.Services.AddScoped<ITokenService, TokenServices>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();



//conxeion con base de datos
string stringConnection = Environment.GetEnvironmentVariable("DATABASE_URL") ?? "data_source";
builder.Services.AddDbContext<ApplicationDBContext>(opt => opt.UseSqlite(stringConnection));


//App builder
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDBContext>();
    await context.Database.MigrateAsync();
}

app.MapControllers(); 
app.UseAuthentication(); 
app.UseAuthorization();  
app.UseHttpsRedirection();
app.Run();


