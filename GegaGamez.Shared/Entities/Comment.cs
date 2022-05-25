namespace GegaGamez.Shared.Entities;

public partial class Comment : IEntity
{
    public DateTime CreatedAt { get; set; }
    public virtual Game Game { get; set; } = null!;
    public int GameId { get; set; }
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public virtual User User { get; set; } = null!;
    public int UserId { get; set; }
}
