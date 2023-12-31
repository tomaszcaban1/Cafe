using Cafe.Application.Hosts.Commands;
using Cafe.Application.Hosts.Queries.GetAllHosts;
using Cafe.Contracts.Host;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using Host = Cafe.Domain.Aggregates.HostAggregate.Host;

namespace Cafe.Api.Controllers;

[Route("api/[Controller]")]
public class HostController : ApiBaseController
{
    readonly ISender _mediator;
    readonly IMapper _mapper;

    public HostController(ISender mediator, IMapper mapper) : base(mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var command = new GetAllHostsQuery();
        var getAllHostsResult = await _mediator.Send(command);

        return MatchResultGet<List<Host>, List<HostResponse>>(getAllHostsResult);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateHostRequest request)
    {
        var command = _mapper.Map<CreateHostCommand>(request);
        var createHostResult = await _mediator.Send(command);

        return MatchResultPost<Host, HostResponse>(createHostResult, "register");
    }
}
