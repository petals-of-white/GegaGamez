namespace GegaGamez.WebUI.Models.Display;

public record class RatingModel
{
    public UserModel User { get; set; }
    public GameModel Game { get; set; }
    public byte RatingScore { get; set; }
}
