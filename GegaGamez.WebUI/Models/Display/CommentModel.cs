namespace GegaGamez.WebUI.Models.Display;

public record class CommentModel
{
    public int Id { get; set; }

    //public int GameId { get; set; }
    //public int UserId { get; set; }
    public UserModel User { get; set; }
    public GameModel Game { get; set; }
    public string Text { get; set; }
}
