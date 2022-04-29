namespace GegaGamez.Shared.Entities;

public class Game
{
    public Game()
    {
        Comments = new HashSet<Comment>();
        Ratings = new HashSet<Rating>();
        DefaultCollections = new HashSet<DefaultCollection>();
        Genres = new HashSet<Genre>();
        UserCollections = new HashSet<UserCollection>();
    }
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public DateTime ReleaseDate { get; set; }
    public string Description { get; set; } = null!;
    public int DeveloperId { get; set; }
    public decimal Price { get; set; }
    public Developer Developer { get; set; } = null!;
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Rating> Ratings { get; set; }
    public ICollection<DefaultCollection> DefaultCollections { get; set; }
    public ICollection<Genre> Genres { get; set; }
    public ICollection<UserCollection> UserCollections { get; set; }
}
