
using BibliotecaServiceAPI.Repositorios.Interfaces;
using BibliotecaServiceAPI.Repositorios;
using Microsoft.EntityFrameworkCore;
using BibliotecaServiceAPI.Data;

namespace BibliotecaServiceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<BibliotecaServiceAPIDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase") ?? throw new InvalidOperationException("Connection string 'DB_BibliotecaServiceAPI' not found.")));

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddScoped<ILivroRepositorio, LivroRepositorio>();
            builder.Services.AddScoped<IEmprestimoRepositorio, EmprestimoRepositorio>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
