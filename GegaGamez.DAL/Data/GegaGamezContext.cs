using GegaGamez.DAL.Data.EntityConfigurations;
using GegaGamez.Shared.Entities;
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
