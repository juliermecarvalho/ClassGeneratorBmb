using Bmb.Teste.Operation.MasterData.Domain.Example.Entities.v1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bmb.Teste.Operation.MasterData.Infra.Data.Configurations.v1;

public class ExampleConfiguration : IEntityTypeConfiguration<Example>
{
    public void Configure(EntityTypeBuilder<Example> builder)
    {
        builder.ToTable("tbl_example");

        builder.HasKey(pk => pk.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.PropertyOne)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.Property(p => p.PropertyTwo)
            .IsRequired()
            .HasColumnType("bit");

        builder.Property(p => p.IsActive)
            .IsRequired()
            .HasColumnType("bit");
    }
}