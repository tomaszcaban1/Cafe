using FluentValidation;

namespace Cafe.Application.Authentication.Queries.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty();

        RuleFor(c => c.Password)
            .NotEmpty();
    }
}
