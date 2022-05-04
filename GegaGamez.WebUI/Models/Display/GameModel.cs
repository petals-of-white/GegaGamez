namespace GegaGamez.WebUI.Models.Display;

public record class GameModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public string Description { get; set; }
    public DeveloperModel Developer { get; set; }
    public byte AvgRatingScore { get; set; }
}
