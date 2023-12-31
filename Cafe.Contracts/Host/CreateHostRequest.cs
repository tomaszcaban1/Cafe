namespace Cafe.Contracts.Host;

public record CreateHostRequest(
    string UserId,
    string Name
);