using TesteJSL.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TesteJSL.Infra.Data.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.DataCadastro)
                .HasColumnType("DateTime")
                .IsRequired(true)
                .HasDefaultValueSql("GETDATE()");

            builder
                .Property(p => p.Login)
                .HasColumnType("nvarchar(150)")
                .IsRequired(true);

            builder
               .Property(p => p.Senha)
               .HasColumnType("nvarchar(150)")
               .IsRequired(true);
        }
    }
}
