using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GegaGamez.DAL.Data.EntityConfigurations;

internal class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable("Genre");

        builder.HasIndex(e => e.Name, "NIX_Genre_Name")
            .IsUnique();

        builder.Property(e => e.Description).HasMaxLength(100);

        builder.Property(e => e.Name).HasMaxLength(50);
    }
}
