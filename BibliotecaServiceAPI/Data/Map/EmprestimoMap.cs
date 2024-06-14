using BibliotecaServiceAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibliotecaServiceAPI.Data.Map
{
    public class EmprestimoMap : IEntityTypeConfiguration<EmprestimoModel>

    {
        public void Configure(EntityTypeBuilder<EmprestimoModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DataEmprestimo).IsRequired().HasMaxLength(255);
            builder.Property(x => x.DataDevolucao).IsRequired().HasMaxLength(150);

            builder.Property(x => x.LivroId).IsRequired();
            builder.HasOne(x => x.Livro);
            builder.Property(x => x.StatusLivro);
        }
    }
}
