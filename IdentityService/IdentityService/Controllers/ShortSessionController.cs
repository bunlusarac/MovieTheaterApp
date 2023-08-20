using IdentityService.Commands;
using IdentityService.DTOs;
using IdentityService.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers;

[ApiController]
[AllowAnonymous]
[Route("short-session")]
public class ShortSessionController: ControllerBase
{
    private readonly IMediator _mediator;

    public ShortSessionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create/{userId:guid}")]
    public async Task<ShortSessionCreatedDto> CreateShortSession(Guid userId)
    {
        var cmd = new CreateShortSessionCommand(userId);
        return await _mediator.Send(cmd);
    }
    
    [HttpPost("validate/{userId:guid}")]
    public async Task<ShortSessionValidityDto> ValidateShortSession(Guid userId, [FromBody] ValidateShortSessionDto dto)
    {
        var cmd = new ValidateShortSessionQuery(dto.ShortSessionToken, userId);
        return await _mediator.Send(cmd);
    }
}