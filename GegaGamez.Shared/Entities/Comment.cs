namespace GegaGamez.Shared.Entities;

public class Comment
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int UserId { get; set; }
    public string Text { get; set; } = null!;
    public Game Game { get; set; } = null!;
    public User User { get; set; } = null!;
}
