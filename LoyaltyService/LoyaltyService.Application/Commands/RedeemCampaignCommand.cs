using LoyaltyService.Application.DTOs;
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
    public Task Handle(RedeemCampaignCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}