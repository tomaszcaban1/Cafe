using Cafe.Domain.Aggregates.UserAggregate.ValueObjects;
using Cafe.Domain.Common.Models;

namespace Cafe.Domain.Aggregates.HostAggregate.ValueObjects;

public sealed class HostId : AggregateRootId<string>
{
    HostId(string value)
    {
        Value = value;
    }

    HostId()
    {
    }

    public override string Value { get; protected set;  }

    public static HostId Create(string hostId) => new(hostId);

    public static HostId Create(UserId userId) => new HostId($"Host_{userId.Value}");

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}