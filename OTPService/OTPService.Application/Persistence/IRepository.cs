using OTPService.Application.Common;
using OTPService.Domain.Common;

namespace OTPService.Application.Persistence;

public interface IRepository<T> where T: AggregateRoot
{
    public T? GetById(Guid id);
    public IList<T>? GetAll();
    public Result Set(T entity);
}