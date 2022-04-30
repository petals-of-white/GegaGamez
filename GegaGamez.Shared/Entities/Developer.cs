namespace GegaGamez.Shared.Entities;

public class Developer
{
    public Developer()
    {
        Games = new HashSet<Game>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ICollection<Game> Games { get; set; }
}
