using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GegaGamez.DAL.Data.EntityConfigurations;

internal class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("Game");

        builder.HasIndex(e => e.DeveloperId, "NIX_Game_DeveloperId");

        builder.HasIndex(e => e.ReleaseDate, "NIX_Game_ReleaseDate");

        builder.HasIndex(e => e.Title, "NIX_Game_Title");

        builder.Property(e => e.Description).HasMaxLength(1000);

        builder.Property(e => e.ReleaseDate).HasColumnType("date");

        builder.Property(e => e.Title).HasMaxLength(100);

        builder.HasOne(d => d.Developer)
            .WithMany(p => p.Games)
            .HasForeignKey(d => d.DeveloperId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Game_Developer");

        builder.HasMany(d => d.Genres)
            .WithMany(p => p.Games)
            .UsingEntity<GameGenre>(
                "GameGenre",

                right => right
                    .HasOne<Genre>()
                    .WithMany()
                    .HasForeignKey(j => j.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Games_Genres_GenreID"),

                left => left
                    .HasOne<Game>()
                    .WithMany()
                    .HasForeignKey(j => j.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Games_Genres_GameId"),

                join =>
                {
                    join.ToTable("Games_Genres")
                        .HasKey(j => new { j.GenreId, j.GameId });

                    join.HasIndex(j => new { j.GenreId }, "NIX_Games_Genres_GenreId");
                }
            );
    }
}
