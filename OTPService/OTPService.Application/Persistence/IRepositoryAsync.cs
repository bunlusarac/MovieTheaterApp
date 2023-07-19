using OTPService.Application.Common;
using OTPService.Domain.Common;

namespace OTPService.Application.Persistence;

public interface IRepositoryAsync<T> where T: AggregateRoot
{
    public Task<T>? GetById(Guid id);
    public Task<IList<T>>? GetAll();
    public Task<Result> Add(T entity);
    public Task<Result> Update(T entity);
}