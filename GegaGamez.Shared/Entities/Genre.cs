namespace GegaGamez.Shared.Entities;

public partial class Genre
{
    public Genre()
    {
        Games = new HashSet<Game>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public virtual ICollection<Game> Games { get; set; }
}
