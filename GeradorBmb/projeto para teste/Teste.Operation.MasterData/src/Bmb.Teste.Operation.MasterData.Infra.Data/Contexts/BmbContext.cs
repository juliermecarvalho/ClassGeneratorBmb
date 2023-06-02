using Bmb.Teste.Operation.MasterData.Domain.Example.Entities.v1;
using Microsoft.EntityFrameworkCore;

namespace Bmb.Teste.Operation.MasterData.Infra.Data.Contexts;

public class BmbContext : DbContext
{
    public BmbContext(DbContextOptions<BmbContext> options) : base (options) { }

    public DbSet<Example> Examples => Set<Example>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BmbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}