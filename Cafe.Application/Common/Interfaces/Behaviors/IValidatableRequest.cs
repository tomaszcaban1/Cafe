using MediatR;

namespace Cafe.Application.Common.Interfaces.Behaviors;

// The out keyword indicates that TResponse is covariant,
// which means it can only be used as a return type and not as an input parameter type in the interface’s methods.
public interface IValidatableRequest<out TResponse> : IRequest<TResponse>, IValidatableRequest { }

public interface IValidatableRequest { }