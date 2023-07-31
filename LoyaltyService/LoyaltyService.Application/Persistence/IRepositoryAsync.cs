using LoyaltyService.Domain.Common;

namespace LoyaltyService.Application.Persistence;

public interface IRepositoryAsync<T> where T : AggregateRoot
{
    public Task<List<T>> GetAll();
    public Task<T> GetById();
    public Task Update(T entity);
    public Task Add(T entity);
    public Task DeleteById(T entity);
}