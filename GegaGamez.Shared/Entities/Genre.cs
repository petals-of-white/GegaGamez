namespace GegaGamez.Shared.Entities;

public class Genre
{
    public Genre()
    {
        Games = new HashSet<Game>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ICollection<Game> Games { get; set; }
}
