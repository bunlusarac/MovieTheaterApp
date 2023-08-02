using LoyaltyService.Application.Persistence;
using LoyaltyService.Domain.Utils;
using LoyaltyService.Domain.ValueObjects;
using MediatR;

namespace LoyaltyService.Application.Commands;

public class UpdateCampaignCommand : IRequest
{
    public Guid CampaignId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public CampaignType Type { get; set; }
    public double DiscountRate { get; set; }
    public int MaxRedeems { get; set; }
    public DateTime ExpirationDate { get; set; }

    public UpdateCampaignCommand(Guid campaignId, string name, string description, decimal cost, CampaignType type, double discountRate, int maxRedeems, DateTime expirationDate)
    {
        CampaignId = campaignId;
        Name = name;
        Description = description;
        Cost = cost;
        Type = type;
        DiscountRate = discountRate;
        MaxRedeems = maxRedeems;
        ExpirationDate = expirationDate;
    }
}

public class UpdateCampaignCommandHandler : IRequestHandler<UpdateCampaignCommand>
{
    private readonly ICampaignRepository _campaignRepository;

    public UpdateCampaignCommandHandler(ICampaignRepository campaignRepository)
    {
        _campaignRepository = campaignRepository;
    }

    public async Task Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
    {
        var campaign = await _campaignRepository.GetById(request.CampaignId);
        
        campaign.AssignCampaignDetails(
            request.Name,
            request.Description,
            request.Cost,
            request.Type,
            request.DiscountRate,
            request.MaxRedeems,
            request.ExpirationDate);

        await _campaignRepository.Update(campaign);
    }
}