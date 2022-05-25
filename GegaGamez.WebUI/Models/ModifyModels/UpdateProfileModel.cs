namespace GegaGamez.WebUI.Models.ModifyModels;

public record class UpdateProfileModel
{
    public int Id { get; set; }
    public int CountryId { get; set; }
    public string About { get; set; }
}
