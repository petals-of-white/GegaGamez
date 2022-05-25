namespace GegaGamez.Shared.Entities;

public partial class DefaultCollectionType : IEntity
{
    public DefaultCollectionType()
    {
        DefaultCollections = new HashSet<DefaultCollection>();
    }

    public virtual ICollection<DefaultCollection> DefaultCollections { get; set; }
    public string? Description { get; set; }
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
