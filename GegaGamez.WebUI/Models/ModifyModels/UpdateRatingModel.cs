namespace GegaGamez.WebUI.Models.ModifyModels;

public record class UpdateRatingModel
{
    public byte RatingScore { get; set; }
    public int GameId { get; set; }
    public int UserId { get; set; }
}
