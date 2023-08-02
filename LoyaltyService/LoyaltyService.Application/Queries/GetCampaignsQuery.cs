using LoyaltyService.Application.DTOs;
using LoyaltyService.Application.Persistence;
using MediatR;

namespace LoyaltyService.Application.Queries;

public class GetCampaignsQuery: IRequest<IEnumerable<CampaignDto>>
{
    
}

public class GetCampaignsQueryHandler : IRequestHandler<GetCampaignsQuery, IEnumerable<CampaignDto>>
{
    private readonly ICampaignRepository _campaignRepository;

    public GetCampaignsQueryHandler(ICampaignRepository campaignRepository)
    {
        _campaignRepository = campaignRepository;
    }

    public async Task<IEnumerable<CampaignDto>> Handle(GetCampaignsQuery request, CancellationToken cancellationToken)
    {
        var campaigns = await _campaignRepository.GetAll();

        return campaigns.Select(c => new CampaignDto
        {
            Name = c.Name,
            Description = c.Description,
            Cost = c.Cost.Amount,
            Type = c.Type,
            DiscountRate = c.DiscountRate,
            MaxRedeems = c.MaxRedeems,
            ExpirationDate = c.ExpirationDate,
            CampaignId = c.Id
        });
    }
}
