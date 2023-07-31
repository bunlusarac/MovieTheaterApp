using LoyaltyService.Domain.Utils;
using LoyaltyService.Domain.ValueObjects;
using MediatR;

namespace LoyaltyService.Application.Commands;

public class UpdateCampaignCommand : IRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public PointsAmount Cost { get; set; }
    public CampaignType Type { get; set; }
    public double DiscountRate { get; set; }
    public int MaxRedeems { get; set; }
    public DateTime ExpirationDate { get; set; }

    public UpdateCampaignCommand(string name, string description, PointsAmount cost, CampaignType type, double discountRate, int maxRedeems, DateTime expirationDate)
    {
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
    public Task Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}