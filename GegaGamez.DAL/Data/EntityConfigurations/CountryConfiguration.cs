using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GegaGamez.DAL.Data.EntityConfigurations;

internal class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("Country");

        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.Property(e => e.Name)
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property(e => e.ThreeCharCode)
            .HasMaxLength(3)
            .IsUnicode(false)
            .IsFixedLength();

        builder.Property(e => e.TwoCharCode)
            .HasMaxLength(2)
            .IsUnicode(false)
            .IsFixedLength();
    }
}
