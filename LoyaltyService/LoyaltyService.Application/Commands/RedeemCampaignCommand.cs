using LoyaltyService.Application.DTOs;
using LoyaltyService.Application.Persistence;
using LoyaltyService.Domain.Entities;
using LoyaltyService.Domain.ValueObjects;
using MediatR;

namespace LoyaltyService.Application.Commands;

public class RedeemCampaignCommand : IRequest
{
    public Guid CampaignId { get; set; }
    public Guid CustomerId { get; set; }

    public RedeemCampaignCommand(Guid campaignId, Guid customerId)
    {
        CampaignId = campaignId;
        CustomerId = customerId;
    }
}

public class RedeemCampaignCommandHandler : IRequestHandler<RedeemCampaignCommand>
{
    private readonly ILoyaltyCustomerRepository _loyaltyCustomerRepository;
    private readonly ICampaignRepository _campaignRepository;

    public RedeemCampaignCommandHandler(ILoyaltyCustomerRepository loyaltyCustomerRepository, ICampaignRepository campaignRepository)
    {
        _loyaltyCustomerRepository = loyaltyCustomerRepository;
        _campaignRepository = campaignRepository;
    }

    public async Task Handle(RedeemCampaignCommand request, CancellationToken cancellationToken)
    {
        var loyaltyCustomer = await _loyaltyCustomerRepository.GetByCustomerId(request.CustomerId);
        var campaign = await _campaignRepository.GetById(request.CampaignId);
        
        loyaltyCustomer.RedeemCampaign(campaign);
        await _loyaltyCustomerRepository.Update(loyaltyCustomer);
    }
}