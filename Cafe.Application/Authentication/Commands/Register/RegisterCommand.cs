using Cafe.Application.Authentication.Common;
using Cafe.Application.Common.Interfaces.Behaviors;
using ErrorOr;

namespace Cafe.Application.Authentication.Commands.Register;

public record RegisterCommand(string FirstName,
                              string LastName,
                              string Email,
                              string Password) : IValidatableRequest<ErrorOr<AuthenticationResult>>;
