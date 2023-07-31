using LoyaltyService.Domain.Entities;

namespace LoyaltyService.Application.Persistence;

public interface ILoyaltyCustomerRepository: IRepositoryAsync<LoyaltyCustomer>
{
    Task<LoyaltyCustomer> GetByCustomerId(Guid customerId);
}