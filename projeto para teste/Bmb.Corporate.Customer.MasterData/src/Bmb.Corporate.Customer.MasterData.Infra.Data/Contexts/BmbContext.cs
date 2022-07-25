using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Entities.v1;
using Bmb.Corporate.Customer.MasterData.Domain.Segment.Entities.v1;
using Microsoft.EntityFrameworkCore;

namespace Bmb.Corporate.Customer.MasterData.Infra.Data.Contexts;

public class BmbContext : DbContext
{
    public BmbContext(DbContextOptions<BmbContext> options) : base(options) { }

    public DbSet<Segment> Segments => Set<Segment>();

    public DbSet<MinhaClasse> MinhaClasses => Set<MinhaClasse>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BmbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
