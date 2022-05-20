using FluentValidation;
using GegaGamez.WebUI.Models.Auth;

namespace GegaGamez.WebUI.Models.Validation
{
    public class LoginValidator : AbstractValidator<LoginUserModel>
    {
        public LoginValidator()
        {
            RuleFor(r => r.Username).Length(6, 30);
            RuleFor(r => r.Password).Length(6, 30);
        }
    }
}
