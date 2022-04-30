using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GegaGamez.DAL.Data.EntityConfigurations;

internal class DeveloperConfiguration : IEntityTypeConfiguration<Developer>
{
    public void Configure(EntityTypeBuilder<Developer> builder)
    {
        builder.ToTable("Developer");

        builder.HasIndex(e => e.Name, "NIX_Developer_Name");

        builder.Property(e => e.BeginDate).HasColumnType("date");

        builder.Property(e => e.Description).HasMaxLength(500);

        builder.Property(e => e.EndDate).HasColumnType("date");

        builder.Property(e => e.Name).HasMaxLength(100);
    }
}
