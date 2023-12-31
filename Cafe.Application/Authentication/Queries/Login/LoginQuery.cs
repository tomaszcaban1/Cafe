using Cafe.Application.Authentication.Common;
using Cafe.Application.Common.Interfaces.Behaviors;
using ErrorOr;

namespace Cafe.Application.Authentication.Queries.Login;

public record LoginQuery(string Email, string Password) : IValidatableRequest<ErrorOr<AuthenticationResult>>;
