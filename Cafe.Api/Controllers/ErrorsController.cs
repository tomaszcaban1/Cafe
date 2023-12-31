using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.Api.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error() => 
        HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error switch
        {
            OperationCanceledException ex => Problem(title: ex.Source,
                       statusCode: StatusCodes.Status400BadRequest,
                       detail: ex.Message),
            Exception ex => Problem(title: ex.Source,
                       statusCode: StatusCodes.Status500InternalServerError,
                       detail: ex.Message),
            _ => Problem(title: "Unknown error",
                       statusCode: StatusCodes.Status500InternalServerError,
                       detail: "Unknown error")
        };
}
