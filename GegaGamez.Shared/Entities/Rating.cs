namespace GegaGamez.Shared.Entities;

public partial class Rating
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int GameId { get; set; }
    public byte RatingScore { get; set; }

    public virtual Game Game { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
