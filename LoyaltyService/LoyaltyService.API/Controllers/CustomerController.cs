using LoyaltyService.API.DTOs;
using LoyaltyService.Application.Commands;
using LoyaltyService.Application.DTOs;
using LoyaltyService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyService.API.Controllers;

[ApiController]
[Route("customer")]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{customerId:guid}/redeems")]
    public async Task<IEnumerable<RedeemDto>> GetCustomerRedeems(Guid customerId)
    {
        var command = new GetCustomerRedeemsQuery(customerId);
        return await _mediator.Send(command);
    }
    
    [HttpGet("{customerId:guid}/wallet")]
    public async Task<WalletDto> GetCustomerWallet(Guid customerId)
    {
        var command = new GetCustomerWalletQuery(customerId);
        return await _mediator.Send(command);
    }

    [HttpPost("")]
    public async Task<LoyaltyCustomerCreatedDto> RegisterLoyaltyCustomer(
        [FromBody] RegisterLoyaltyCustomerDto dto)
    {
        var command = new RegisterLoyaltyCustomerCommand(dto.CustomerId);
        return await _mediator.Send(command);
    }
    
    [HttpPut("{customerId:guid}/wallet/deposit")]
    public async Task DepositToCustomerWallet(
        Guid customerId,
        [FromBody] DepositToWalletDto dto)
    {
        var command = new DepositToWalletCommand(customerId, dto.PointsAmount);
        await _mediator.Send(command);
    }
    
    [HttpPut("{customerId:guid}/wallet/withdraw")]
    public async Task WithdrawFromCustomerWallet(
        Guid customerId,
        [FromBody] WithdrawFromWalletDto dto)
    {
        var command = new WithdrawFromWalletCommand(customerId, dto.PointsAmount);
        await _mediator.Send(command);
    }
    
    [HttpPut("{customerId:guid}/redeem/{campaignId:guid}")]
    public async Task<CampaignRedeemedDto> RedeemCampaignForCustomer(Guid customerId, Guid campaignId, [FromBody] RedeemCampaignWithVersioningDto dto)
    {
        var command = new RedeemCampaignCommand(campaignId, customerId, dto.Version);
        return await _mediator.Send(command);
    }
    
    [HttpPut("{customerId:guid}/refund/{redeemId:guid}")]
    public async Task RefundCampaignForCustomer(Guid customerId, Guid redeemId)
    {
        var command = new RefundCampaignCommand(customerId, redeemId);
        await _mediator.Send(command);
    }
}