using LoyaltyService.Application.DTOs;
using MediatR;

namespace LoyaltyService.Application.Queries;

public class GetCampaignsQuery: IRequest<List<CampaignDto>>
{
    
}

public class GetCampaignsQueryHandler : IRequestHandler<GetCampaignsQuery, List<CampaignDto>>
{
    public Task<List<CampaignDto>> Handle(GetCampaignsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
