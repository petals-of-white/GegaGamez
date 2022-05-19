using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GegaGamez.DAL.Data.EntityConfigurations;

internal class UserCollectionConfiguration : IEntityTypeConfiguration<UserCollection>
{
    public void Configure(EntityTypeBuilder<UserCollection> builder)
    {
        builder.ToTable("UserCollection");

        builder.HasIndex(e => new { e.UserId, e.Name }, "NIX_UserCollection_UserId_Name")
            .IsUnique();

        builder.Property(e => e.Description).HasMaxLength(100);

        builder.Property(e => e.Name).HasMaxLength(50);

        builder.HasOne(d => d.User)
            .WithMany(p => p.UserCollections)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_UserComzllection_User");

        builder.HasMany(d => d.Games)
            .WithMany(p => p.UserCollections)
            .UsingEntity<UserCollectionGame>(
                "UserCollectionGame",

                left => left
                    .HasOne<Game>()
                    .WithMany()
                    .HasForeignKey(join => join.GameId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_GamesInUserCollections_Game"),

                right => right
                    .HasOne<UserCollection>()
                    .WithMany()
                    .HasForeignKey(join => join.UserCollectionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_GamesInUserCollections_Collection"),

                join => join
                    .ToTable("GamesInUserCollections")
                    .HasKey(join => new { join.UserCollectionId, join.GameId })
                    .HasName("PK__tmp_ms_x__E3149A325890C568")
            );
    }
}
