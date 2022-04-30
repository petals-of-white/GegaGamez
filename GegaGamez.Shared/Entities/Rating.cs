namespace GegaGamez.Shared.Entities;

public class Rating
{
    public int UserId { get; set; }
    public int GameId { get; set; }
    public byte RatingScore { get; set; }
    public Game Game { get; set; } = null!;
    public User User { get; set; } = null!;
}
