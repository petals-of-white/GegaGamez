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
            .OnDelete(DeleteBehavior.SetNull) // that's right
            .HasConstraintName("FK_User_Country");

        builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<UserRole>(
                "UserRole",

                right => right
                    .HasOne<Role>()
                    .WithMany()
                    .HasForeignKey(u => u.RoleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_UserRole_Role"),

                left => left
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey(u => u.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_UserRole_User"),

                join => join
                    .ToTable("UserRole")
                    .HasKey(join => new { join.UserId, join.RoleId })
                    .HasName("PK_UserRole")
            );
    }
}
