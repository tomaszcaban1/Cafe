using Cafe.Domain.Aggregates.UserAggregate;

namespace Cafe.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
