namespace GegaGamez.Shared.Entities;

public class DefaultCollectionType
{
    public DefaultCollectionType()
    {
        DefaultCollections = new HashSet<DefaultCollection>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ICollection<DefaultCollection> DefaultCollections { get; set; }
}
