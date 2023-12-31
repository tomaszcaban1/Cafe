using Cafe.Application.Common.Interfaces.Persistence;
using Cafe.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Cafe.Application.Common.Interfaces.Authentication;
using Cafe.Application.Authentication.Common;
using Microsoft.AspNetCore.Identity;
using Cafe.Domain.Aggregates.UserAggregate;

namespace Cafe.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    readonly IUserRepository _userRepository;
    readonly IJwtTokenGenerator _jwtTokenGenerator;
    readonly IPasswordHasher<User> _passwordHasher;

    public LoginQueryHandler(IUserRepository userRepository,
                             IJwtTokenGenerator jwtTokenGenerator,
                             IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken ct)
    {
        if (await(_userRepository.GetByEmailNoTracking(query.Email, ct)) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, query.Password);
        if (passwordVerificationResult is PasswordVerificationResult.Failed)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}
