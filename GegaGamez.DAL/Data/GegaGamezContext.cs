using GegaGamez.DAL.Data.EntityConfigurations;
using GegaGamez.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Data
{
    public partial class GegaGamezContext : DbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                   .ApplyConfiguration(new CommentConfiguration())
                   .ApplyConfiguration(new CountryConfiguration())
                   .ApplyConfiguration(new DefaultCollectionConfiguration())
                   .ApplyConfiguration(new DefaultCollectionTypeConfiguration())
                   .ApplyConfiguration(new DeveloperConfiguration())
                   .ApplyConfiguration(new GameConfiguration())
                   .ApplyConfiguration(new GenreConfiguration())
                   .ApplyConfiguration(new RatingConfiguration())
                   .ApplyConfiguration(new UserConfiguration())
                   .ApplyConfiguration(new UserCollectionConfiguration());

            /*
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_GameId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_User");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ThreeCharCode).IsFixedLength();

                entity.Property(e => e.TwoCharCode).IsFixedLength();
            });

            modelBuilder.Entity<DefaultCollection>(entity =>
            {
                entity.HasOne(d => d.DefaultCollectionType)
                    .WithMany(p => p.DefaultCollections)
                    .HasForeignKey(d => d.DefaultCollectionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DefaultCollection_DefaultCollectionType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DefaultCollections)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DefaultCollection_User");
            });

            modelBuilder.Entity<DefaultCollectionType>(entity =>
            {
                entity.HasMany(d => d.Games)
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
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasOne(d => d.Developer)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.DeveloperId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Game_Developer");

                entity.HasMany(d => d.Genres)
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

                entity.HasMany(d => d.UserCollections)
                    .WithMany(p => p.Games)
                    .UsingEntity<Dictionary<string, object>>(
                        "GamesInUserCollection",
                        l => l.HasOne<UserCollection>().WithMany().HasForeignKey("UserCollectionId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_GamesInUserCollections_Collection"),
                        r => r.HasOne<Game>().WithMany().HasForeignKey("GameId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_GamesInUserCollections_Game"),
                        j =>
                        {
                            j.HasKey("GameId", "UserCollectionId").HasName("PK__GamesInU__E8A366C9457A57E6");

                            j.ToTable("GamesInUserCollections");
                        });
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RatingScore, e.GameId });

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rating_Game");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rating_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_User_Country");
            });

            modelBuilder.Entity<UserCollection>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCollections)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserCollection_User");
            });
            */
            OnModelCreatingPartial(modelBuilder);
        }

        public GegaGamezContext()
        {
        }

        public GegaGamezContext(DbContextOptions<GegaGamezContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<DefaultCollection> DefaultCollections { get; set; } = null!;
        public virtual DbSet<DefaultCollectionType> DefaultCollectionTypes { get; set; } = null!;
        public virtual DbSet<Developer> Developers { get; set; } = null!;
        public virtual DbSet<Game> Games { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Rating> Ratings { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserCollection> UserCollections { get; set; } = null!;
    }
}
