namespace GegaGamez.Shared.Entities;

public partial class Rating : IEntity
{
    public virtual Game Game { get; set; } = null!;
    public int GameId { get; set; }
    public int Id { get; set; }
    public byte RatingScore { get; set; }
    public virtual User User { get; set; } = null!;
    public int UserId { get; set; }
}
