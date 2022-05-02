namespace GegaGamez.WebUI.Models.Auth;

public record class LoginUserModel
{
    public string Username { get; init; }
    public string Password { get; init; }
}
