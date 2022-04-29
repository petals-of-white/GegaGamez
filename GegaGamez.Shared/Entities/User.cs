namespace GegaGamez.Shared.Entities;

public class User
{
    public User()
    {
        Comments = new HashSet<Comment>();
        DefaultCollections = new HashSet<DefaultCollection>();
        Ratings = new HashSet<Rating>();
        UserCollections = new HashSet<UserCollection>();
    }

    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Name { get; set; }
    public int? CountryId { get; set; }
    public string? About { get; set; }
    public Country? Country { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<DefaultCollection> DefaultCollections { get; set; }
    public ICollection<Rating> Ratings { get; set; }
    public ICollection<UserCollection> UserCollections { get; set; }
}
