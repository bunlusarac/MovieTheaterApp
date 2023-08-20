using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using LoyaltyService.Application.DTOs;
using LoyaltyService.Application.Persistence;
using LoyaltyService.Application.Utils;
using MediatR;

namespace LoyaltyService.Application.Queries;

public class GetCampaignWithVersioningQuery: IRequest<CampaignWithVersioningDto>
{
    public Guid CampaignId { get; set; }

    public GetCampaignWithVersioningQuery(Guid campaignId)
    {
        CampaignId = campaignId;
    }
}

public class GetCampaignWithVersioningQueryHandler : IRequestHandler<GetCampaignWithVersioningQuery, CampaignWithVersioningDto>
{
    private readonly ICampaignRepository _campaignRepository;

    public GetCampaignWithVersioningQueryHandler(ICampaignRepository campaignRepository)
    {
        _campaignRepository = campaignRepository;
    }

    public async Task<CampaignWithVersioningDto> Handle(GetCampaignWithVersioningQuery request, CancellationToken cancellationToken)
    {
        //var campaign = await _campaignRepository.GetById(request.CampaignId);
        var campaign = await _campaignRepository.GetByIdWithLockWait(request.CampaignId);

        return new CampaignWithVersioningDto
        {
            Name = campaign.Name,
            Description = campaign.Description,
            Cost = campaign.Cost.Amount,
            Type = campaign.Type,
            DiscountRate = campaign.DiscountRate,
            MaxRedeems = campaign.MaxRedeems,
            ExpirationDate = campaign.ExpirationDate,
            CampaignId = campaign.Id,
            Version = ConcurrencyTokenHelper.GenerateConcurrencyToken(campaign.Version, campaign.ConcurrencySecret)
        };
    }
}