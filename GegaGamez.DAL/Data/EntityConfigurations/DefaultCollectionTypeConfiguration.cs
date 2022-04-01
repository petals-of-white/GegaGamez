using GegaGamez.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GegaGamez.DAL.Data.EntityConfigurations
{
    internal class DefaultCollectionTypeConfiguration : IEntityTypeConfiguration<DefaultCollectionType>
    {
        public void Configure(EntityTypeBuilder<DefaultCollectionType> builder)
        {
            builder.HasMany(d => d.Games)
                    .WithMany(p => p.DefaultCollections)
                    .UsingEntity<Dictionary<string, object>>(
                        "GamesInDefaultCollection",
                        l => l.HasOne<Game>().WithMany().HasForeignKey("GameId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_GamesInDefaultCollections_Game"),
                        r => r.HasOne<DefaultCollectionType>().WithMany().HasForeignKey("DefaultCollectionId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_GamesInDefaultCollections_DefaultCollection"),
                        j =>
                        {
                            j.HasKey("DefaultCollectionId", "GameId").HasName("PK__GamesInD__F961CBB389938DA8");

                            j.ToTable("GamesInDefaultCollections");
                        });
        }
    }
}