namespace GegaGamez.Shared.Entities;

public partial class Game : IEntity
{
    public Game()
    {
        Comments = new HashSet<Comment>();
        Ratings = new HashSet<Rating>();
        DefaultCollections = new HashSet<DefaultCollection>();
        Genres = new HashSet<Genre>();
        UserCollections = new HashSet<UserCollection>();
    }

    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<DefaultCollection> DefaultCollections { get; set; }
    public string Description { get; set; } = null!;
    public virtual Developer Developer { get; set; } = null!;
    public int DeveloperId { get; set; }
    public virtual ICollection<Genre> Genres { get; set; }
    public int Id { get; set; }
    public virtual ICollection<Rating> Ratings { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Title { get; set; } = null!;
    public virtual ICollection<UserCollection> UserCollections { get; set; }
}
