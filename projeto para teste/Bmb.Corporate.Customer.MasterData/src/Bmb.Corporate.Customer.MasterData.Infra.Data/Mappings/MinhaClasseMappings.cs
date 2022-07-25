using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Entities.v1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bmb.Corporate.Customer.MasterData.Infra.Data.Mappings;

internal class MinhaClasseMapping : IEntityTypeConfiguration<MinhaClasse>
{


     public void Configure(EntityTypeBuilder<MinhaClasse> builder)
    {
        builder.ToTable("tbl_minhaclasse");

        builder.HasKey(pk => pk.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(p => p.IsActive)
            .HasColumnType("char(1)")
            .HasColumnName("is_active")
            .IsRequired();

    }
}
