using Microsoft.EntityFrameworkCore;
using TesteJSL.Domain.Entities;

namespace TesteJSL.Infra.Data.Context
{
    public partial class MotoristaContext : DbContext
    {
        public MotoristaContext(DbContextOptions options) : base(options) { }       

        public DbSet<Veiculo> Veiculo { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Motorista> Motorista { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MotoristaContext).Assembly);
        }
    }
}
