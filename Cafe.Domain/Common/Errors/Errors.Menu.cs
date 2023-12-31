using ErrorOr;

namespace Cafe.Domain.Common.Errors;

public static partial class Errors
{
    public static class Menu
    {
        public static Error MissingMenuId => Error.Validation(
                           code: "Menu.MissingMenuId",
                           description: "Menu does not exist");
    }
}
