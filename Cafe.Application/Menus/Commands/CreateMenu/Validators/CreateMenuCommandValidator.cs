using FluentValidation;

namespace Cafe.Application.Menus.Commands.CreateMenu.Validators;

public class CreateMenuCommandValidator : AbstractValidator<CreateMenuCommand>
{
    public CreateMenuCommandValidator()
    {
        RuleFor(x => x.HostId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(250);

        RuleForEach(x => x.Sections).SetValidator(new CreateMenuSectionCommandValidator());
    }
}