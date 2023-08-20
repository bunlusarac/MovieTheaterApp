using LoyaltyService.Application.DTOs;
using LoyaltyService.Application.Persistence;
using LoyaltyService.Application.Utils;
using MediatR;

namespace LoyaltyService.Application.Queries;

public class GetCampaignsWithVersioningQuery: IRequest<IEnumerable<CampaignWithVersioningDto>>
{
    
}

public class GetCampaignsWithVersioningQueryHandler : IRequestHandler<GetCampaignsWithVersioningQuery, IEnumerable<CampaignWithVersioningDto>>
{
    private readonly ICampaignRepository _campaignRepository;

    public GetCampaignsWithVersioningQueryHandler(ICampaignRepository campaignRepository)
    {
        _campaignRepository = campaignRepository;
    }

    public async Task<IEnumerable<CampaignWithVersioningDto>> Handle(GetCampaignsWithVersioningQuery request, CancellationToken cancellationToken)
    {
        var campaigns = await _campaignRepository.GetAllWithLockWait();

        return campaigns.Select(c => new CampaignWithVersioningDto
        {
            Name = c.Name,
            Description = c.Description,
            Cost = c.Cost.Amount,
            Type = c.Type,
            DiscountRate = c.DiscountRate,
            MaxRedeems = c.MaxRedeems,
            ExpirationDate = c.ExpirationDate,
            CampaignId = c.Id,
            Version = ConcurrencyTokenHelper.GenerateConcurrencyToken(c.Version, c.ConcurrencySecret)
        });
    }
}
