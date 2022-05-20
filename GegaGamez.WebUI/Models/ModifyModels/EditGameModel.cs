namespace GegaGamez.WebUI.Models.ModifyModels;

public record class EditGameModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public string Description { get; set; }
    public int DeveloperId { get; set; }
    public HashSet<int> GenreIds { get; set; }
}
