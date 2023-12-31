using Cafe.Application.Common.Interfaces.Behaviors;
using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Reflection;
using ValidationException = FluentValidation.ValidationException;

namespace Cafe.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    // only allow requests that implement IValidatableRequest
    where TRequest : IValidatableRequest
    where TResponse : IErrorOr
{
    readonly IValidator<TRequest> _validator;

    public ValidationBehavior(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request,
                                        RequestHandlerDelegate<TResponse> next,
                                        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        return validationResult.IsValid 
            ? await next() 
            : getValidationResponse(validationResult);
    }

    static TResponse getValidationResponse(ValidationResult validationResult) => 
        createResponseFromErrors(validationResult.Errors, out var response)
            ? response
            : throw new ValidationException(validationResult.Errors);

    static bool createResponseFromErrors(List<ValidationFailure> validationFailures, out TResponse response)
    {
        List<Error> errors = validationFailures.ConvertAll(x => Error.Validation(
                code: x.PropertyName,
                description: x.ErrorMessage));

        response = (TResponse?)typeof(TResponse)
            .GetMethod(
                name: nameof(ErrorOr<object>.From),
                bindingAttr: BindingFlags.Static | BindingFlags.Public,
                types: new[] { typeof(List<Error>) })?
            .Invoke(null, new[] { errors })!;

        return response is not null;
    }
}