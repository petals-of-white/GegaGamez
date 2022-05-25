namespace GegaGamez.Shared.Entities;

public partial class User : IEntity
{
    public User()
    {
        Comments = new HashSet<Comment>();
        DefaultCollections = new HashSet<DefaultCollection>();
        Ratings = new HashSet<Rating>();
        UserCollections = new HashSet<UserCollection>();
        Roles = new HashSet<Role>();
    }

    public string? About { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
    public virtual Country? Country { get; set; }
    public int? CountryId { get; set; }
    public virtual ICollection<DefaultCollection> DefaultCollections { get; set; }
    public int Id { get; set; }
    public string? Name { get; set; }
    public string Password { get; set; } = null!;
    public virtual ICollection<Rating> Ratings { get; set; }
    public virtual ICollection<Role> Roles { get; set; }
    public virtual ICollection<UserCollection> UserCollections { get; set; }
    public string Username { get; set; } = null!;
}
