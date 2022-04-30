using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GegaGamez.DAL.Data.EntityConfigurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasIndex(e => e.Username, "NIX_User_Username")
            .IsUnique();

        builder.Property(e => e.About).HasMaxLength(500);

        builder.Property(e => e.Name).HasMaxLength(100);

        builder.Property(e => e.Password).HasMaxLength(512);

        builder.Property(e => e.Username).HasMaxLength(50);

        builder.HasOne(d => d.Country)
            .WithMany(p => p.Users)
            .HasForeignKey(d => d.CountryId)
            .HasConstraintName("FK_User_Country");
    }
}
