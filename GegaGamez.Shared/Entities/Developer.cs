namespace GegaGamez.Shared.Entities
{
    public partial class Developer : IEntity
    {
        public Developer()
        {
            Games = new HashSet<Game>();
        }

        public DateTime BeginDate { get; set; }
        public string Description { get; set; } = null!;
        public DateTime? EndDate { get; set; }
        public virtual ICollection<Game> Games { get; set; }
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
