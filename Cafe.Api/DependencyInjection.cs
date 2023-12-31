using Cafe.Api.Common.Mapping;
using Microsoft.OpenApi.Models;

namespace Cafe.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection @this)
    {
        @this.AddControllers();
        @this.AddEndpointsApiExplorer();
        @this.AddMapping();

        addSwager(@this);

        return @this;
    }

    static void addSwager(IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Cafe API", Version = "v1" });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}
