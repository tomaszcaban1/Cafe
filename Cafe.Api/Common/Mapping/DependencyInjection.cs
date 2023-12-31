using Mapster;
using MapsterMapper;
using System.Reflection;

namespace Cafe.Api.Common.Mapping;

public static class DependencyInjection
{
    public static IServiceCollection AddMapping(this IServiceCollection @this)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        // Register all types that inherit from IRegister
        config.Scan(Assembly.GetExecutingAssembly());

        @this.AddSingleton(config);
        @this.AddScoped<IMapper, ServiceMapper>();

        return @this;
    }
}
