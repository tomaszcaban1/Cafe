using Cafe.Application.Common.Interfaces.Services;

namespace Cafe.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
