namespace GegaGamez.Shared.Entities;

public partial class Country : IEntity
{
    public Country()
    {
        Users = new HashSet<User>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ThreeCharCode { get; set; } = null!;
    public string TwoCharCode { get; set; } = null!;
    public virtual ICollection<User> Users { get; set; }
}
