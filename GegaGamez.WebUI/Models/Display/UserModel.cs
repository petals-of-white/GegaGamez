namespace GegaGamez.WebUI.Models.Display;

public record class UserModel
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string? Name { get; set; }
    public CountryModel? Country { get; set; }
    public string? About { get; set; }
}
