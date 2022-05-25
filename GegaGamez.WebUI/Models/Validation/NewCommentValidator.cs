using FluentValidation;
using GegaGamez.WebUI.Models.ModifyModels;

namespace GegaGamez.WebUI.Models.Validation;

public class NewCommentValidator : AbstractValidator<NewCommentModel>
{
    public NewCommentValidator()
    {
        RuleFor(comment => comment.UserId).NotNull();
        RuleFor(comment => comment.GameId).NotNull();
        RuleFor(comment => comment.Text).MaximumLength(1000).NotEmpty();
    }
}
