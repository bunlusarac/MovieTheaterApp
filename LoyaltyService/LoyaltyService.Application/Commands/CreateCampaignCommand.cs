using LoyaltyService.Application.DTOs;
using LoyaltyService.Application.Persistence;
using LoyaltyService.Application.Utils;
using LoyaltyService.Domain.Entities;
using LoyaltyService.Domain.Utils;
using LoyaltyService.Domain.ValueObjects;
using MediatR;

namespace LoyaltyService.Application.Commands;

public class CreateCampaignCommand: IRequest<CampaignCreatedDto>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public CampaignType Type { get; set; }
    public double DiscountRate { get; set; }
    public int MaxRedeems { get; set; }
    public DateTime ExpirationDate { get; set; }

    public CreateCampaignCommand(string name, string description, decimal cost, CampaignType type, double discountRate, int maxRedeems, DateTime expirationDate)
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
    private readonly ICampaignRepository _campaignRepository;

    public CreateCampaignCommandHandler(ICampaignRepository campaignRepository)
    {
        _campaignRepository = campaignRepository;
    }

    public async Task<CampaignCreatedDto> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
    {
        var campaign = new Campaign(
            request.Name,
            request.Description,
            request.Cost,
            request.Type,
            request.DiscountRate,
            request.MaxRedeems,
            request.ExpirationDate);

        campaign.ConcurrencySecret = ConcurrencyTokenHelper.GenerateConcurrencySecret();

        await _campaignRepository.Add(campaign);

        return new CampaignCreatedDto
        {
            CampaignId = campaign.Id
        };
    }
}