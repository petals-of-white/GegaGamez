using FluentValidation;
using GegaGamez.WebUI.Models.ModifyModels;

namespace GegaGamez.WebUI.Models.Validation;

public class EditGameValidator : AbstractValidator<EditGameModel>
{
    public EditGameValidator()
    {
        //RuleFor(g => g.Id).NotEmpty();
        RuleFor(g => g.Title).NotEmpty().MaximumLength(100);
        RuleFor(g => g.Description).NotEmpty().MaximumLength(1000);
        RuleFor(g => g.ReleaseDate).NotNull();
        RuleFor(g => g.DeveloperId).NotEmpty();
    }
}
