using FluentValidation;

namespace Cafe.Application.Menus.Commands.CreateMenu.Validators;

public class CreateMenuItemCommandValidator : AbstractValidator<CreateMenuItemCommand>
{
    public CreateMenuItemCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(x => x.ItemPrice.Amount)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ItemPrice.Currency)
            .NotEmpty()
            .MaximumLength(3);
    }
}
