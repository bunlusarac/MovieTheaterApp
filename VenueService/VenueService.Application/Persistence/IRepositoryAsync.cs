using VenueService.Domain.Common;

namespace VenueService.Application.Persistence;

public interface IRepositoryAsync<T> where T: AggregateRoot
{
    Task<T> GetById(Guid id);
    Task<IList<T>> GetAll();
    Task<T> Update(T entity);
    Task<T> Add(T entity);
}