using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GegaGamez.DAL.Data.EntityConfigurations;

internal class UserCollectionConfiguration : IEntityTypeConfiguration<UserCollection>
{
    public void Configure(EntityTypeBuilder<UserCollection> builder)
    {
        builder.HasOne(d => d.User)
                .WithMany(p => p.UserCollections)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserCollection_User");

        builder.HasMany(d => d.Games)
            .WithMany(p => p.UserCollections)
            .UsingEntity<Dictionary<string, object>>(
                "GamesInUserCollection",
                l => l.HasOne<Game>().WithMany().HasForeignKey("GameId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_GamesInUserCollections_Game"),
                r => r.HasOne<UserCollection>().WithMany().HasForeignKey("UserCollectionId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_GamesInUserCollections_Collection"),
                j =>
                {
                    j.HasKey("UserCollectionId", "GameId").HasName("PK__tmp_ms_x__E3149A325890C568");

                    j.ToTable("GamesInUserCollections");
                });
    }
}
