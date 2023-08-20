using LoyaltyService.API.DTOs;
using LoyaltyService.Application.Commands;
using LoyaltyService.Application.DTOs;
using LoyaltyService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyService.API.Controllers;

[ApiController]
[Route("campaign")]
public class CampaignController : ControllerBase
{
    private readonly IMediator _mediator;

    public CampaignController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("")]
    public async Task<IEnumerable<CampaignDto>> GetAllCampaigns()
    {
        return await _mediator.Send(new GetCampaignsQuery());
    }
    
    [HttpGet("with-versioning")]
    public async Task<IEnumerable<CampaignWithVersioningDto>> GetAllCampaignsWithVersioning()
    {
        return await _mediator.Send(new GetCampaignsWithVersioningQuery());
    }
    
    [HttpGet("{campaignId:guid}/with-versioning")]
    public async Task<CampaignWithVersioningDto> GetCampaignWithVersioning(Guid campaignId)
    {
        return await _mediator.Send(new GetCampaignWithVersioningQuery(campaignId));
    }
    
    [HttpPost("")]
    public async Task<CampaignCreatedDto> CreateCampaign([FromBody] CreateCampaignDto dto)
    {
        var command = new CreateCampaignCommand
        (
            dto.Name,
            dto.Description,
            dto.Cost,
            dto.Type,
            dto.DiscountRate,
            dto.MaxRedeems,
            dto.ExpirationDate
        );
        
        return await _mediator.Send(command);
    }
    
    [HttpDelete("{campaignId:guid}")]
    public async Task DeleteCampaign(Guid campaignId)
    {
        var command = new DeleteCampaignCommand(campaignId);
        await _mediator.Send(command);
    }
    
    [HttpPut("{campaignId:guid}")]
    public async Task UpdateCampaign(Guid campaignId, [FromBody] UpdateCampaignDto dto)
    {
        var command = new UpdateCampaignCommand(
            campaignId,
            dto.Name,
            dto.Description,
            dto.Cost,
            dto.Type,
            dto.DiscountRate,
            dto.MaxRedeems,
            dto.ExpirationDate);
        
        await _mediator.Send(command);
    }
}