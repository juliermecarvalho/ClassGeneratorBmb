using Bmb.Corporate.Customer.MasterData.Domain.Segment.Entities.v1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bmb.Corporate.Customer.MasterData.Infra.Data.Mappings
{
    internal class SegmentMapping : IEntityTypeConfiguration<Segment>
    {
        public void Configure(EntityTypeBuilder<Segment> builder)
        {

            builder.ToTable("tbl_segment");

            builder.HasKey(pk => pk.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasColumnType("varchar(50)")
                .HasColumnName("name")
                .IsRequired();

            builder.Property(p => p.IsActive)
               .HasColumnType("char(1)")
               .HasColumnName("is_active")
               .IsRequired();

            builder.Property(p => p.Abbreviations)
            .HasColumnType("char(3)")
            .HasColumnName("abbreviations")
            .IsRequired();

            builder.Property(p => p.UserId)
            .HasColumnType("varchar(8)")
            .HasColumnName("user_id")
            .IsRequired();

            builder.Property(p => p.Date)
           .HasColumnType("date")
           .HasColumnName("date")
           .IsRequired();
        }
    }
}
