using Bmb.Corporate.Customer.MasterData.Domain.Segment.Entities.v1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bmb.Corporate.Customer.MasterData.Infra.Data.Configurations.v1;

public class SegmentConfiguration : IEntityTypeConfiguration<Segment>
{
    public void Configure(EntityTypeBuilder<Segment> builder)
    {

        builder.ToTable("tbl_segment");

        builder.HasKey(pk => pk.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasColumnType("varchar(50)");

        builder.Property(p => p.Abbreviations)
            .HasColumnType("char(3)");
        builder.Property(p => p.IsActive)
            .HasColumnType("char(1)");

        builder.Property(p => p.UserId)
            .HasColumnType("varchar(8)");
    }
}