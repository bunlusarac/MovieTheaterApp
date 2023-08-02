using LoyaltyService.Application.Persistence;
using LoyaltyService.Domain.Entities;
using LoyaltyService.Domain.Exceptions;
using LoyaltyService.Persistence.Contexts;
using LoyaltyService.Persistence.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyService.Persistence.Repositories;

public class LoyaltyCustomerRepository: RepositoryAsync<LoyaltyCustomer>, ILoyaltyCustomerRepository
{
    public LoyaltyCustomerRepository(DbContextBase<LoyaltyCustomer> context) : base(context)
    {
    }

    public async Task<LoyaltyCustomer> GetByCustomerId(Guid customerId)
    {
        var entity = (await _context.DataSet.Where(c => c.CustomerId == customerId).ToListAsync()).FirstOrDefault();

        if (entity == null)
            throw new LoyaltyPersistenceException(LoyaltyPersistenceErrorCode.NotFound);

        return entity;
    }
}