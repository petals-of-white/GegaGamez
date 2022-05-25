using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GegaGamez.DAL.Data.EntityConfigurations;

internal class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.HasKey(r => r.Id);

        builder.ToTable("Rating");

        builder.HasIndex(r => new { r.UserId, r.GameId, r.RatingScore }, "NIX_Rating_UserId_GameId_RatingScore")
            .IsUnique();
        builder.HasIndex(e => e.GameId, "NIX_Rating_GameId");

        builder.HasIndex(e => e.RatingScore, "NIX_Rating_RatingScore");

        builder.HasOne(d => d.Game)
            .WithMany(p => p.Ratings)
            .HasForeignKey(d => d.GameId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Rating_Game");

        builder.HasOne(d => d.User)
            .WithMany(p => p.Ratings)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Rating_User");
    }
}
