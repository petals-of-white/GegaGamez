namespace GegaGamez.WebUI.Models.Display;

public record class CommentModel
{
    public int Id { get; set; }
    public UserModel User { get; set; }
    public GameModel Game { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
}
