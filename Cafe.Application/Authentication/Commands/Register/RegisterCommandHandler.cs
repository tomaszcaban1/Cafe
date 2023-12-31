using Cafe.Application.Common.Interfaces.Authentication;
using Cafe.Application.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;
using Cafe.Application.Authentication.Common;
using Microsoft.AspNetCore.Identity;
using Cafe.Domain.Common.Errors;
using Cafe.Domain.Aggregates.UserAggregate;

namespace Cafe.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    readonly IUserRepository _userRepository;
    readonly IJwtTokenGenerator _jwtTokenGenerator;
    readonly IPasswordHasher<User> _passwordHasher;

    public RegisterCommandHandler(IUserRepository userRepository,
                                  IJwtTokenGenerator jwtTokenGenerator,
                                  IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken ct)
    {
        if (await _userRepository.IsEmailTaken(command.Email, ct))
        {
            return Errors.Registration.NotRegistered;
        }

        User user = createUser(command);

        await _userRepository.Add(user, ct);

        var token = _jwtTokenGenerator.GenerateToken(user);

        var result = new AuthenticationResult(user, token);

        return result;
    }

    User createUser(RegisterCommand command)
    {
        var user = User.Create(command.FirstName, command.LastName, command.Email, command.Password);

        var hashedPassword = _passwordHasher.HashPassword(user, command.Password);

        User.AddHashedPassword(user, hashedPassword);

        return user;
    }
}
