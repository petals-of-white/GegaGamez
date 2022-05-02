namespace GegaGamez.WebUI.Models.ModifyModels;

public record class UpdateProfileModel
{
    //public string Username { get; set; }
    //public string? Name { get; set; }
    public int Id { get; set; }
    public int CountryId { get; set; }
    public string About { get; set; }
}
