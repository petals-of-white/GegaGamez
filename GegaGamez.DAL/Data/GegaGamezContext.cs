using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Data;

public partial class GegaGamezContext : DbContext
{
    private partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(GetType().Assembly);

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
    public virtual DbSet<UserCollection> UserCollections { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;
}
