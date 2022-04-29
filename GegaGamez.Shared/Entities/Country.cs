namespace GegaGamez.Shared.Entities
{
    public class Country
    {
        public Country()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string TwoCharCode { get; set; } = null!;
        public string ThreeCharCode { get; set; } = null!;
        public ICollection<User> Users { get; set; }
    }
}
