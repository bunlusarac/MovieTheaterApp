using LoyaltyService.Application.DTOs;
using LoyaltyService.Domain.Utils;
using LoyaltyService.Domain.ValueObjects;
using MediatR;

namespace LoyaltyService.Application.Commands;

public class CreateCampaignCommand: IRequest<CampaignCreatedDto>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public PointsAmount Cost { get; set; }
    public CampaignType Type { get; set; }
    public double DiscountRate { get; set; }
    public int MaxRedeems { get; set; }
    public DateTime ExpirationDate { get; set; }

    public CreateCampaignCommand(string name, string description, PointsAmount cost, CampaignType type, double discountRate, int maxRedeems, DateTime expirationDate)
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

public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, CampaignCreatedDto>
{
    public Task<CampaignCreatedDto> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}