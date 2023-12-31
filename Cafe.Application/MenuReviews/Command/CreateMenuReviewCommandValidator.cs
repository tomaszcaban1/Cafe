using Cafe.Application.Common.Extensions;
using FluentValidation;

namespace Cafe.Application.MenuReviews.Command;

public class CreateMenuReviewCommandValidator : AbstractValidator<CreateMenuReviewCommand>
{
    public CreateMenuReviewCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty()
            .Must(x => x.IsValidGuid())
            .WithMessage("UserId must be a valid GUID.");

        RuleFor(c => c.MenuId)
            .NotEmpty()
            .Must(x => x.IsValidGuid())
            .WithMessage("MenuId must be a valid GUID.");
    }
}
