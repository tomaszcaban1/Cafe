using FluentValidation;

namespace Cafe.Application.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(c => c.LastName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(50);

        RuleFor(c => c.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(50)
            .Matches("[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches("[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches("(\\d)+").WithMessage("Your password must contain at least one number.")
            .Matches("(\\W)+").WithMessage("Your password must contain at least one special character.");
    }
}
