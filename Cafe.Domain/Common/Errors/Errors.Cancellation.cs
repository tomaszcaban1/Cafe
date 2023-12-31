using ErrorOr;

namespace Cafe.Domain.Common.Errors;

public static partial class Errors
{
    public static class Cancellation
    {
        public static Error RequestCancelled => Error.Validation(
                           code: "Cancellation.RequestCancelled",
                           description: "Request cancelled");
    }
}
