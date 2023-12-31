using Cafe.Application.Hosts.Commands;
using Cafe.Contracts.Host;
using Mapster;
using Host = Cafe.Domain.Aggregates.HostAggregate.Host;

namespace Cafe.Api.Common.Mapping;

public class HostMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateHostRequest, CreateHostCommand>();

        config.NewConfig<Host, HostResponse>()
            .Map(dest => dest.HostId, src => src.Id.Value.ToString());
    }
}
