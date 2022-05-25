namespace GegaGamez.Shared.Entities;

public partial class UserCollection : IEntity
{
    public UserCollection()
    {
        Games = new HashSet<Game>();
    }

    public string? Description { get; set; }
    public virtual ICollection<Game> Games { get; set; }
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual User User { get; set; } = null!;
    public int UserId { get; set; }
}
