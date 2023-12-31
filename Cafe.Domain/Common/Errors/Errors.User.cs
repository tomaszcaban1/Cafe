using ErrorOr;

namespace Cafe.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error MissingUserId => Error.Validation(
                           code: "User.MissingUserId",
                           description: "User does not exist");
    }
}
