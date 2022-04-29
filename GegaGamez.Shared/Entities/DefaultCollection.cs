namespace GegaGamez.Shared.Entities;

public class DefaultCollection
{
    public DefaultCollection()
    {
        Games = new HashSet<Game>();
    }

    public int Id { get; set; }
    public int UserId { get; set; }
    public int DefaultCollectionTypeId { get; set; }
    public DefaultCollectionType DefaultCollectionType { get; set; } = null!;
    public User User { get; set; } = null!;
    public ICollection<Game> Games { get; set; }
}
