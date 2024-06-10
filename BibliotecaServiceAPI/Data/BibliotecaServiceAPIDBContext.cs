using BibliotecaServiceAPI.Data.Map;
using BibliotecaServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaServiceAPI.Data
{
    public class BibliotecaServiceAPIDBContext : DbContext
    {
        public BibliotecaServiceAPIDBContext(DbContextOptions<BibliotecaServiceAPIDBContext> options)
         : base(options)
        {

        }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<LivroModel> Livros { get; set; }
        public DbSet<EmprestimoModel> Emprestimos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new LivroMap());
            modelBuilder.ApplyConfiguration(new EmprestimoMap());
            base.OnModelCreating(modelBuilder);
        }
    } 
}
