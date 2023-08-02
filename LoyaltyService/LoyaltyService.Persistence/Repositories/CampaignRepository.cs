using LoyaltyService.Application.Persistence;
using LoyaltyService.Domain.Entities;
using LoyaltyService.Persistence.Contexts;

namespace LoyaltyService.Persistence.Repositories;

public class CampaignRepository: RepositoryAsync<Campaign>, ICampaignRepository
{
    public CampaignRepository(DbContextBase<Campaign> context) : base(context)
    {
    }
}