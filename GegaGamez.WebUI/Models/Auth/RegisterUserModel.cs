namespace GegaGamez.WebUI.Models.Auth;

public record class RegisterUserModel
{
    public string Username { get; init; }
    public string Password { get; init; }
    public string ConfirmPassword { get; init; }
    public bool PasswordsMatch => Password == ConfirmPassword;
}
