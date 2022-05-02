namespace GegaGamez.WebUI.Models.Display;

public record struct DeveloperModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly BeginDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string Description { get; set; }
}
