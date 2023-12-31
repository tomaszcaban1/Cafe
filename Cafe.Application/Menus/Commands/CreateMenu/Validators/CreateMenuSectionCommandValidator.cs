using FluentValidation;

namespace Cafe.Application.Menus.Commands.CreateMenu.Validators;

public class CreateMenuSectionCommandValidator : AbstractValidator<CreateMenuSectionCommand>
{
    public CreateMenuSectionCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(250);

        RuleForEach(x => x.Items).SetValidator(new CreateMenuItemCommandValidator());
    }
}
