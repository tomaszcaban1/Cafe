using Cafe.Application.Common.Interfaces.Authentication;
using Cafe.Application.Common.Interfaces.Persistence;
using Cafe.Application.Common.Interfaces.Services;
using Cafe.Infrastructure.Authentication;
using Cafe.Infrastructure.Persistence;
using Cafe.Infrastructure.Persistence.Interceptors;
using Cafe.Infrastructure.Persistence.Repositories;
using Cafe.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Cafe.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection @this, IConfiguration configuration)
    {
        @this
            .addPersistance(configuration)
            .addAuthentication(configuration);

        @this.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return @this;
    }

    static IServiceCollection addPersistance(this IServiceCollection @this, IConfiguration configuration)
    {
        @this.AddDbContext<CafeDbContext>(options => options
            .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        @this.AddScoped<PublishDomainEventsInterceptor>();
        @this.AddScoped<IUserRepository, UserRepository>();
        @this.AddScoped<IMenuRepository, MenuRepository>();
        @this.AddScoped<IHostRepository, HostRepository>();
        @this.AddScoped<IMenuReviewRepository, MenuReviewRepository>();

        return @this;
    }

    static IServiceCollection addAuthentication(this IServiceCollection @this, IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        @this.AddSingleton(Options.Create(jwtSettings));
        @this.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        // Add authentication services, what is validated in token
        @this.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(cfg =>
        {
            cfg.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtSettings.Issuer,

                ValidateAudience = true,
                ValidAudience = jwtSettings.Audience,

                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            };
        });

        return @this;
    }
}