using LoyaltyService.Application.DTOs;
using LoyaltyService.Application.Exceptions;
using LoyaltyService.Application.Persistence;
using LoyaltyService.Application.Utils;
using LoyaltyService.Domain.Entities;
using LoyaltyService.Domain.Exceptions;
using LoyaltyService.Domain.ValueObjects;
using MediatR;

namespace LoyaltyService.Application.Commands;

public class RedeemCampaignCommand : IRequest<CampaignRedeemedDto>
{
    public Guid CampaignId { get; set; }
    public Guid CustomerId { get; set; }
    public string ConcurrencyToken { get; set; }

    public RedeemCampaignCommand(Guid campaignId, Guid customerId, string concurrencySecret)
    {
        CampaignId = campaignId;
        CustomerId = customerId;
        ConcurrencyToken = concurrencySecret;
    }
}

public class RedeemCampaignCommandHandler : IRequestHandler<RedeemCampaignCommand,CampaignRedeemedDto>
{
    private readonly ILoyaltyCustomerRepository _loyaltyCustomerRepository;
    private readonly ICampaignRepository _campaignRepository;

    public RedeemCampaignCommandHandler(ILoyaltyCustomerRepository loyaltyCustomerRepository, ICampaignRepository campaignRepository)
    {
        _loyaltyCustomerRepository = loyaltyCustomerRepository;
        _campaignRepository = campaignRepository;
    }

    public async Task<CampaignRedeemedDto> Handle(RedeemCampaignCommand request, CancellationToken cancellationToken)
    {
        var loyaltyCustomer = await _loyaltyCustomerRepository.GetByCustomerId(request.CustomerId);
        var campaign = await _campaignRepository.GetById(request.CampaignId);

        var versionIsValid = ConcurrencyTokenHelper.ValidateConcurrencyToken(campaign.Version, campaign.ConcurrencySecret,
            request.ConcurrencyToken);

        if (!versionIsValid)
            throw new LoyaltyApplicationException(LoyaltyApplicationErrorCode.VersionExpired);
        
        var redeem = loyaltyCustomer.RedeemCampaign(campaign);
        
        await _loyaltyCustomerRepository.Update(loyaltyCustomer);

        return new CampaignRedeemedDto
        {
            RedeemId = redeem.Id
        };
    }
}