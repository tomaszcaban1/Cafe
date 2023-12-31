using Cafe.Application.Authentication.Commands.Register;
using Cafe.Application.Authentication.Common;
using Cafe.Application.Authentication.Queries.Login;
using Cafe.Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.Api.Controllers;

[Route("api/[controller]")]
[AllowAnonymous]
public class AuthenticationController : ApiBaseController
{
    // Instead of using IMediator, use ISender, which is a simpler interface
    readonly ISender _mediator;
    readonly IMapper _mapper;

    public AuthenticationController(ISender mediator, IMapper mapper) : base(mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken ct)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        var authenticationResult = await _mediator.Send(command, ct);

        return MatchResultPost<AuthenticationResult, AuthenticationResponse>(authenticationResult, "register");
    }

    [HttpGet("login")]
    public async Task<IActionResult> Login([FromQuery]LoginRequest request, CancellationToken ct)
    {
        var query = _mapper.Map<LoginQuery>(request);
        var loginResult = await _mediator.Send(query, ct);

        return MatchResultGet<AuthenticationResult, AuthenticationResponse>(loginResult);
    }
}