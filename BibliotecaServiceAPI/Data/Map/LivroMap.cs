using BibliotecaServiceAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibliotecaServiceAPI.Data.Map
{
    public class LivroMap : IEntityTypeConfiguration<LivroModel>

    {
        public void Configure(EntityTypeBuilder<LivroModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Titulo).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Autor).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Isbn).HasMaxLength(150);
            builder.Property(x => x.Ano);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.UsuarioId);
            builder.HasOne(x => x.Usuario);
        }
    }
}
