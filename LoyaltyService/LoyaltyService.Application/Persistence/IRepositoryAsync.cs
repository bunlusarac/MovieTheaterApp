using LoyaltyService.Domain.Common;
using LoyaltyService.Domain.Entities;

namespace LoyaltyService.Application.Persistence;

public interface IRepositoryAsync<T> where T : AggregateRoot
{
    public Task<List<T>> GetAll();
    public Task<T> GetById(Guid entityId);
    public Task Update(T entity);
    public Task Add(T entity);
    public Task DeleteById(Guid entityId);
}