namespace GegaGamez.Shared.Entities;

public class UserCollection
{
    public UserCollection()
    {
        Games = new HashSet<Game>();
    }

    public int Id { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public User User { get; set; } = null!;

    public ICollection<Game> Games { get; set; }
}
