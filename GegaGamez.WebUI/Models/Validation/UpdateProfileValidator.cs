using FluentValidation;
using GegaGamez.WebUI.Models.ModifyModels;

namespace GegaGamez.WebUI.Models.Validation;

public class UpdateProfileValidator : AbstractValidator<UpdateProfileModel>
{
    public UpdateProfileValidator()
    {
        RuleFor(profile => profile.Id).NotEmpty();
        RuleFor(profile => profile.CountryId).NotEmpty();
        RuleFor(profile => profile.About).MaximumLength(500).NotEmpty();
    }
}
