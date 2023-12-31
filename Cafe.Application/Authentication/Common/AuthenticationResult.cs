using Cafe.Domain.Aggregates.UserAggregate;

namespace Cafe.Application.Authentication.Common;

public record AuthenticationResult(User User, string Token);