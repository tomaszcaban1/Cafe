using Cafe.Application.Authentication.Commands.Register;
using Cafe.Application.Authentication.Common;
using Cafe.Application.Authentication.Queries.Login;
using Cafe.Contracts.Authentication;
using Mapster;

namespace Cafe.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();

        config.NewConfig<LoginRequest, LoginQuery>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Id, src => src.User.Id.Value.ToString())
            .Map(dest => dest.FirstName, src => src.User.FirstName)
            .Map(dest => dest.LastName, src => src.User.LastName)
            .Map(dest => dest.Email, src => src.User.Email)
            .Map(dest => dest.Token, src => src.Token);
    }
}
