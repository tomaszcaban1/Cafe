using Cafe.Application.Authentication.Commands.Register;
using Cafe.Application.Common.Behaviors;
using Cafe.Domain.Aggregates.UserAggregate;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Cafe.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection @this)
    {
        @this.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<RegisterCommand>());

        // Registering the generic ValidationBehavior, which implement IValidatableRequest 
        @this.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        // Registering the validators
        @this.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        @this.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        return @this;
    }
}
