using ErrorOr;

namespace Cafe.Domain.Common.Errors;

public static partial class Errors
{
    public static class Registration
    {
        public static Error NotRegistered => Error.Validation(
                code: "Registration.NotRegistered",
                description: "Invalid email or password"
        );

        public static Error UserNotUnique => Error.Validation(
                code: "Registration.NotRegistered",
                description: "UserId is not unique"
        );
    }
}
