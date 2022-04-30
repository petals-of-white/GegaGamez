namespace GegaGamez.Shared.Entities
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; } = null!;

        public virtual Game Game { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
