using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Data;

public partial class GegaGamezContext : DbContext
{
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GegaGamez;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(GetType().Assembly);

        /*
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("Comment");

            entity.HasIndex(e => e.GameId, "NIX_Comment_GameId");

            entity.Property(e => e.Text).HasMaxLength(1000);

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
            entity.ToTable("Country");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ThreeCharCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();

            entity.Property(e => e.TwoCharCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<DefaultCollection>(entity =>
        {
            entity.ToTable("DefaultCollection");

            entity.HasIndex(e => new { e.UserId, e.DefaultCollectionTypeId }, "NIX_DefaultCollection_UserId_DefaultCollectionTypeId")
                .IsUnique();

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

            entity.HasMany(d => d.Games)
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
        });

        modelBuilder.Entity<DefaultCollectionType>(entity =>
        {
            entity.ToTable("DefaultCollectionType");

            entity.HasIndex(e => e.Name, "NIX_DefaultCollectionType_Name")
                .IsUnique();

            entity.Property(e => e.Description).HasMaxLength(500);

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Developer>(entity =>
        {
            entity.ToTable("Developer");

            entity.HasIndex(e => e.Name, "NIX_Developer_Name");

            entity.Property(e => e.BeginDate).HasColumnType("date");

            entity.Property(e => e.Description).HasMaxLength(500);

            entity.Property(e => e.EndDate).HasColumnType("date");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.ToTable("Game");

            entity.HasIndex(e => e.DeveloperId, "NIX_Game_DeveloperId");

            entity.HasIndex(e => e.ReleaseDate, "NIX_Game_ReleaseDate");

            entity.HasIndex(e => e.Title, "NIX_Game_Title");

            entity.Property(e => e.Description).HasMaxLength(1000);

            entity.Property(e => e.ReleaseDate).HasColumnType("date");

            entity.Property(e => e.Title).HasMaxLength(100);

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

                        j.HasIndex(new[] { "GenreId" }, "NIX_Games_Genres_GenreId");
                    });
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("Genre");

            entity.HasIndex(e => e.Name, "NIX_Genre_Name")
                .IsUnique();

            entity.Property(e => e.Description).HasMaxLength(100);

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RatingScore, e.GameId });

            entity.ToTable("Rating");

            entity.HasIndex(e => e.GameId, "NIX_Rating_GameId");

            entity.HasIndex(e => e.RatingScore, "NIX_Rating_RatingScore");

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
            entity.ToTable("User");

            entity.HasIndex(e => e.Username, "NIX_User_Username")
                .IsUnique();

            entity.Property(e => e.About).HasMaxLength(500);

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.Property(e => e.Password).HasMaxLength(512);

            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Country)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_User_Country");
        });

        modelBuilder.Entity<UserCollection>(entity =>
        {
            entity.ToTable("UserCollection");

            entity.HasIndex(e => new { e.UserId, e.Name }, "NIX_UserCollection_UserId_Name")
                .IsUnique();

            entity.Property(e => e.Description).HasMaxLength(100);

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.User)
                .WithMany(p => p.UserCollections)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserCollection_User");

            entity.HasMany(d => d.Games)
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
