namespace GegaGamez.WebUI.Models.ModifyModels;

public record class UpdateDeveloperModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
}
