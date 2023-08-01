using LoyaltyService.Application.Persistence;
using LoyaltyService.Domain.Entities;
using LoyaltyService.Persistence.Contexts;

namespace LoyaltyService.Persistence.Repositories;

public class LoyaltyCustomerRepository: ILoyaltyCustomerRepository
{
    private readonly LoyaltyCustomerDbContext _context;

    public LoyaltyCustomerRepository(LoyaltyCustomerDbContext context)
    {
        _context = context;
    }

    public Task<List<LoyaltyCustomer>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<LoyaltyCustomer> GetById(Guid entityId)
    {
        throw new NotImplementedException();
    }

    public Task Update(LoyaltyCustomer entity)
    {
        throw new NotImplementedException();
    }

    public Task Add(LoyaltyCustomer entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteById(Guid entityId)
    {
        throw new NotImplementedException();
    }

    public Task<LoyaltyCustomer> GetByCustomerId(Guid customerId)
    {
        throw new NotImplementedException();
    }
}