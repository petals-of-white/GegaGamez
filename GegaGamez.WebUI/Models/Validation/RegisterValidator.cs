using FluentValidation;
using GegaGamez.WebUI.Models.Auth;

namespace GegaGamez.WebUI.Models.Validation;

public class RegisterValidator : AbstractValidator<RegisterUserModel>
{
    public RegisterValidator()
    {
        RuleFor(r => r.Username).Length(6, 30);
        RuleFor(r => r.Password).Length(6, 30);
        RuleFor(r => r.ConfirmPassword).Length(6, 30);
        RuleFor(r => r.PasswordsMatch).Must((r, _) => r.PasswordsMatch == true);
    }
}
