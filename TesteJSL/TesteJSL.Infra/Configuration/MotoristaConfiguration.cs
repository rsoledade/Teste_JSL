using TesteJSL.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TesteJSL.Infra.Data.Configuration
{
    public class MotoristaConfiguration : IEntityTypeConfiguration<Motorista>
    {
        public void Configure(EntityTypeBuilder<Motorista> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.DataCadastro)
                .HasColumnType("DateTime")
                .IsRequired(true)
                .HasDefaultValueSql("GETDATE()");

            builder
                .Property(p => p.Nome)
                .HasColumnType("nvarchar(200)")
                .IsRequired(true);

            builder
                .Property(p => p.Sobrenome)
                .HasColumnType("nvarchar(200)")
                .IsRequired(true);

            builder
                .Property(p => p.Endereco)
                .HasColumnType("nvarchar(200)")
                .IsRequired(true);

            builder
                .Property(p => p.Numero)
                .HasColumnType("nvarchar(10)")
                .IsRequired(true);

            builder
                .Property(p => p.Complemento)
                .HasColumnType("nvarchar(200)");

            builder
                .Property(p => p.Cep)
                .HasColumnType("nvarchar(12)")
                .IsRequired(true);

            builder
                .Property(p => p.Bairro)
                .HasColumnType("nvarchar(200)")
                .IsRequired(true);

            builder
                .Property(p => p.Cidade)
                .HasColumnType("nvarchar(200)")
                .IsRequired(true);

            builder
                .Property(p => p.Estado)
                .HasColumnType("nvarchar(2)")
                .IsRequired(true);

            builder
                .Property(p => p.EnderecoLatitude)
                .HasColumnType("nvarchar(50)");

            builder
               .Property(p => p.EnderecoLongitude)
               .HasColumnType("nvarchar(50)");
        }
    }
}
