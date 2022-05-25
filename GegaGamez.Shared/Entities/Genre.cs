namespace GegaGamez.Shared.Entities;

public partial class Genre : IEntity
{
    public Genre()
    {
        Games = new HashSet<Game>();
    }

    public string Description { get; set; } = null!;
    public virtual ICollection<Game> Games { get; set; }
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
