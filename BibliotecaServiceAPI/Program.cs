
using BibliotecaServiceAPI.Repositorios.Interfaces;
using BibliotecaServiceAPI.Repositorios;
using Microsoft.EntityFrameworkCore;
using BibliotecaServiceAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

namespace BibliotecaServiceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string chaveSecreta = "C9AFA24F-D65F-49E5-8583-3BEDADF1A416";
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<BibliotecaServiceAPIDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase") ?? throw new InvalidOperationException("Connection string 'DB_BibliotecaServiceAPI' not found.")));

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BibliotecaService - API", Version = "v1" });
                var securitySchema = new OpenApiSecurityScheme
                {
                    Name = "JWT Autentificação",
                    Description = "Entre com o JWT Bearer token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securitySchema);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
     {
         { securitySchema, new string [] {} }
     });
            });


            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddScoped<ILivroRepositorio, LivroRepositorio>();
            builder.Services.AddScoped<IEmprestimoRepositorio, EmprestimoRepositorio>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {                 
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "sua_empresa",
                    ValidAudience = "sua_aplicacao",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta))
                };
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
