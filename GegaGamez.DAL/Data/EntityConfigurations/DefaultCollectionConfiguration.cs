using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GegaGamez.DAL.Data.EntityConfigurations;

internal class DefaultCollectionConfiguration : IEntityTypeConfiguration<DefaultCollection>
{
    public void Configure(EntityTypeBuilder<DefaultCollection> builder)
    {
        builder.HasOne(d => d.DefaultCollectionType)
                .WithMany(p => p.DefaultCollections)
                .HasForeignKey(d => d.DefaultCollectionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DefaultCollection_DefaultCollectionType");

        builder.HasOne(d => d.User)
            .WithMany(p => p.DefaultCollections)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_DefaultCollection_User");

        builder.HasMany(d => d.Games)
            .WithMany(p => p.DefaultCollections)
            .UsingEntity<Dictionary<string, object>>(
                "GamesInDefaultCollection",
                l => l.HasOne<Game>().WithMany().HasForeignKey("GameId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_GamesInDefaultCollections_Game"),
                r => r.HasOne<DefaultCollection>().WithMany().HasForeignKey("DefaultCollectionId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_GamesInDefaultCollections_DefaultCollection"),
                j =>
                {
                    j.HasKey("DefaultCollectionId", "GameId").HasName("PK__tmp_ms_x__F961CBB3A30D57A3");

                    j.ToTable("GamesInDefaultCollections");
                });
    }
}
