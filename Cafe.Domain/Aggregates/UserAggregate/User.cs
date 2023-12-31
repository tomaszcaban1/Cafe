using Cafe.Domain.Aggregates.UserAggregate.ValueObjects;
using Cafe.Domain.Common.Models;

namespace Cafe.Domain.Aggregates.UserAggregate;

public sealed class User : AggregateRoot<UserId, Guid>
{
    User(string firstName, string lastName, string email, string password, UserId id) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = password;
    }

    User()
    {
    }

    public string FirstName { get; private set; } = default!;

    public string LastName { get; private set; } = default!;

    public string Email { get; private set; } = default!;

    public string PasswordHash { get; private set; } = default!;

    public DateTime CreatedDateTime { get; private set; }

    public DateTime UpdatedDateTime { get; private set; }

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string password)
    {
        return new User(
            firstName,
            lastName,
            email,
            password,
            UserId.CreateUnique());
    }

    public static void AddHashedPassword(User user, string hashedPassword) => 
        user.PasswordHash = hashedPassword;
}
