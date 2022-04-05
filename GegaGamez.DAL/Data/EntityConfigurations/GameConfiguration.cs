﻿using GegaGamez.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GegaGamez.DAL.Data.EntityConfigurations
{
    internal class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasOne(d => d.Developer)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.DeveloperId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Game_Developer");

            builder.HasMany(d => d.Genres)
                .WithMany(p => p.Games)
                .UsingEntity<Dictionary<string, object>>(
                    "GamesGenre",
                    l => l.HasOne<Genre>().WithMany().HasForeignKey("GenreId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Games_Genres_GenreID"),
                    r => r.HasOne<Game>().WithMany().HasForeignKey("GameId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Games_Genres_GameId"),
                    j =>
                    {
                        j.HasKey("GameId", "GenreId");

                        j.ToTable("Games_Genres");

                        j.HasIndex(new [] { "GenreId" }, "NIX_Games_Genres_GenreId");
                    });
        }
    }
}
