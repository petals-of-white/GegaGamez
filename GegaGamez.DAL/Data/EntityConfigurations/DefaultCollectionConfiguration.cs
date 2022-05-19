using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GegaGamez.DAL.Data.EntityConfigurations;

internal class DefaultCollectionConfiguration : IEntityTypeConfiguration<DefaultCollection>
{
    public void Configure(EntityTypeBuilder<DefaultCollection> builder)
    {
        builder.ToTable("DefaultCollection");

        builder.HasIndex(e => new { e.UserId, e.DefaultCollectionTypeId }, "NIX_DefaultCollection_UserId_DefaultCollectionTypeId")
            .IsUnique();

        builder.HasOne(d => d.DefaultCollectionType)
            .WithMany(p => p.DefaultCollections)
            .HasForeignKey(d => d.DefaultCollectionTypeId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_DefaultCollection_DefaultCollectionType");

        builder.HasOne(d => d.User)
            .WithMany(p => p.DefaultCollections)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_DefaultCollection_User");

        builder.HasMany(d => d.Games)
            .WithMany(p => p.DefaultCollections)
            .UsingEntity<DefaultCollectionGame>(
                "DefaultCollectionGame",

                left => left
                    .HasOne<Game>()
                    .WithMany()
                    .HasForeignKey(join => join.GameId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_GamesInDefaultCollections_Game"),

                right => right
                    .HasOne<DefaultCollection>()
                    .WithMany()
                    .HasForeignKey(join => join.DefaultCollectionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_GamesInDefaultCollections_DefaultCollection"),

                join => join
                    .ToTable("GamesInDefaultCollections")
                    .HasKey(s => new { s.DefaultCollectionId, s.GameId }).HasName("PK__tmp_ms_x__F961CBB3A30D57A3")
                );
    }
}
