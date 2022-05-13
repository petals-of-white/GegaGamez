namespace GegaGamez.Shared.Entities;

public partial class DefaultCollection
{
    public DefaultCollection()
    {
        Games = new HashSet<Game>();
    }

    public int Id { get; set; }
    public int UserId { get; set; }
    public int DefaultCollectionTypeId { get; set; }

    public virtual DefaultCollectionType DefaultCollectionType { get; set; } = null!;
    public virtual User User { get; set; } = null!;

    public virtual ICollection<Game> Games { get; set; }
}
