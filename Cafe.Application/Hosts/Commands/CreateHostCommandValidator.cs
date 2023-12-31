using Cafe.Application.Common.Extensions;
using FluentValidation;

namespace Cafe.Application.Hosts.Commands;

public class CreateHostCommandValidator : AbstractValidator<CreateHostCommand>
{ 
    public CreateHostCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty()
            .Must(x => x.IsValidGuid())
            .WithMessage("UserId must be a valid GUID.");

        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}
