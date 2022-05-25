namespace GegaGamez.Shared.Entities;

public partial class DefaultCollection : IEntity
{
    public DefaultCollection()
    {
        Games = new HashSet<Game>();
    }

    public virtual DefaultCollectionType DefaultCollectionType { get; set; } = null!;
    public int DefaultCollectionTypeId { get; set; }
    public virtual ICollection<Game> Games { get; set; }
    public int Id { get; set; }
    public virtual User User { get; set; } = null!;
    public int UserId { get; set; }
}
