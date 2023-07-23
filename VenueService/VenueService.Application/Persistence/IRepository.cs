using VenueService.Domain.Common;

namespace VenueService.Application.Persistence;

public interface IRepository<T> where T: AggregateRoot
{
    T GetById(Guid id);
    IList<T> GetAll();
    T Update(T entity);
    T Add(T entity);
}