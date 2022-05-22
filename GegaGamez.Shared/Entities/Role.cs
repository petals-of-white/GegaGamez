namespace GegaGamez.Shared.Entities;

public partial class Role : IEntity
{
    public Role()
    {
        Users = new HashSet<User>();
    }
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<User> Users { get; set; }
}
