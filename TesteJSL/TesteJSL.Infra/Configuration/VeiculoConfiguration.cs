using System;
using TesteJSL.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TesteJSL.Infra.Data.Configuration
{
    public class VeiculoConfiguration : IEntityTypeConfiguration<Veiculo>
    {
        public void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.DataCadastro)
                .HasColumnType("DateTime")
                .IsRequired(true)
                .HasDefaultValueSql("GETDATE()");

            builder
                .Property(p => p.Marca)
                .HasColumnType("nvarchar(150)")
                .IsRequired(true);

            builder
                .Property(p => p.Modelo)
                .HasColumnType("nvarchar(150)")
                .IsRequired(true);

            builder
                .Property(p => p.Placa)
                .HasColumnType("nvarchar(10)")
                .IsRequired(true);

            builder
                .Property(p => p.Eixos)
                .HasColumnType("tinyint")
                .IsRequired(true);

            builder
                .HasOne(d => d.Motorista)
                .WithMany(p => p.Veiculos)
                .HasForeignKey(p => p.IdMotorista);
        }
    }
}
