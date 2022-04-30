﻿using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GegaGamez.DAL.Data.EntityConfigurations;

internal class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.HasKey(e => new { e.UserId, e.RatingScore, e.GameId });

        builder.ToTable("Rating");

        builder.HasIndex(e => e.GameId, "NIX_Rating_GameId");

        builder.HasIndex(e => e.RatingScore, "NIX_Rating_RatingScore");

        builder.HasOne(d => d.Game)
            .WithMany(p => p.Ratings)
            .HasForeignKey(d => d.GameId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Rating_Game");

        builder.HasOne(d => d.User)
            .WithMany(p => p.Ratings)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Rating_User");
    }
}
