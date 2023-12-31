using ErrorOr;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cafe.Api.Controllers;

[ApiController]
[Authorize]
public class ApiBaseController : ControllerBase
{
    readonly IMapper _mapper;

    public ApiBaseController(IMapper mapper)
    {
        _mapper = mapper;
    }

    public IActionResult MatchResultPost<TIn, TOut>(ErrorOr<TIn> errorOrResult, string action) =>
        errorOrResult.Match(
            result =>
            {
                return result is TIn notNullResult
                    ? CreatedAtAction(action, _mapper.Map<TOut>(notNullResult))
                    : Ok();
            },
            Problem);

    public IActionResult MatchResultGet<TIn, TOut>(ErrorOr<TIn> errorOrResult) =>
        errorOrResult.Match(
             result =>
             {
                 return result is TIn notNullResult 
                    ? Ok(_mapper.Map<TOut>(notNullResult)) 
                    : NotFound();
             },
             Problem);

    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0)
        {
            return Problem(title: "Unknown error",
                           statusCode: StatusCodes.Status500InternalServerError,
                           detail: "Unknown error");
        }

        return errors.All(e => e.Type == ErrorType.Validation)
            ? validationProblem(errors) 
            : problem(errors);
    }

    IActionResult validationProblem(List<Error> errors)
    {
        var modelState = new ModelStateDictionary();

        errors.ForEach(e => modelState.AddModelError(e.Code, e.Description));

        return ValidationProblem(modelState);
    }

    IActionResult problem(List<Error> errors)
    {
        var firstError = errors.First();

        var statusCode = firstError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        var result = Problem(statusCode: statusCode, title: firstError.Description);

        setErrorCodes(errors, result);

        return result;
    }

    void setErrorCodes(List<Error> errors, ObjectResult result)
    {
        var valueProperty = result.GetType()?.GetProperty("Value")?.GetValue(result);

        if(valueProperty is ProblemDetails problemDetails)
        {
            problemDetails.Extensions.Add("errorCodes", errors.Select(e => e.Code).ToList());
        }       
    }
}
