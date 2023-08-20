
using LoyaltyService.Application.Persistence;

using MediatR;

namespace LoyaltyService.Application.Commands;

public class RefundCampaignCommand : IRequest
{
    public Guid CustomerId { get; set; }
    public Guid RedeemId { get; set; }

    public RefundCampaignCommand(Guid customerId, Guid redeemId)
    {
        CustomerId = customerId;
        RedeemId = redeemId;
    }
}

public class RefundCampaignCommandHandler : IRequestHandler<RefundCampaignCommand>
{
    private readonly ILoyaltyCustomerRepository _loyaltyCustomerRepository;
    private readonly ICampaignRepository _campaignRepository;

    public RefundCampaignCommandHandler(ILoyaltyCustomerRepository loyaltyCustomerRepository, ICampaignRepository campaignRepository)
    {
        _loyaltyCustomerRepository = loyaltyCustomerRepository;
        _campaignRepository = campaignRepository;
    }

    public async Task Handle(RefundCampaignCommand request, CancellationToken cancellationToken)
    {
        var loyaltyCustomer = await _loyaltyCustomerRepository.GetByCustomerId(request.CustomerId);

        loyaltyCustomer.RefundCampaign(request.RedeemId);
        await _loyaltyCustomerRepository.Update(loyaltyCustomer);
    }
}