using LoyaltyService.Application.Persistence;
using LoyaltyService.Domain.Entities;
using LoyaltyService.Persistence.Contexts;

namespace LoyaltyService.Persistence.Repositories;

public class CampaignRepository: ICampaignRepository
{
    private readonly CampaignDbContext _context;

    public CampaignRepository(CampaignDbContext context)
    {
        _context = context;
    }

    public Task<List<Campaign>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Campaign> GetById(Guid entityId)
    {
        throw new NotImplementedException();
    }

    public Task Update(Campaign entity)
    {
        throw new NotImplementedException();
    }

    public Task Add(Campaign entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteById(Guid entityId)
    {
        throw new NotImplementedException();
    }
}