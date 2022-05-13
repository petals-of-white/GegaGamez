namespace GegaGamez.WebUI.Models.ModifyModels;

public record class UpdateGameModel
{
    public string Title { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public string Description { get; set; }
    public int DeveloperId { get; set; }
}
