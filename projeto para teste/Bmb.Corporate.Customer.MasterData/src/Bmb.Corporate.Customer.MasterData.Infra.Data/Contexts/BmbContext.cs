using Bmb.Corporate.Customer.MasterData.Domain.Segment.Entities.v1;
using Microsoft.EntityFrameworkCore;

namespace Bmb.Corporate.Customer.MasterData.Infra.Data.Contexts;

public class BmbContext : DbContext
{
    public BmbContext(DbContextOptions<BmbContext> options) : base (options) { }

    public DbSet<Segment> Segments => Set<Segment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BmbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}