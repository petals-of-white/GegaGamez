using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GegaGamez.DAL.Data.EntityConfigurations;

internal class DefaultCollectionTypeConfiguration : IEntityTypeConfiguration<DefaultCollectionType>
{
    public void Configure(EntityTypeBuilder<DefaultCollectionType> builder)
    {
        builder.ToTable("DefaultCollectionType");

        builder.HasIndex(e => e.Name, "NIX_DefaultCollectionType_Name")
            .IsUnique();

        builder.Property(e => e.Description).HasMaxLength(500);

        builder.Property(e => e.Name).HasMaxLength(50);
    }
}
