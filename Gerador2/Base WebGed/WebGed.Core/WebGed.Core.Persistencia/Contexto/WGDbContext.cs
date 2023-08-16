using Microsoft.EntityFrameworkCore;
using WebGed.Core.Dominio.Core;

namespace WebGed.Persistencia.Contexto
{
    public class WGDbContext : DbContext
    {
        public WGDbContext(DbContextOptions<WGDbContext> dbContext)
            : base(dbContext)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Aplicativo> Aplicativos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WGDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
