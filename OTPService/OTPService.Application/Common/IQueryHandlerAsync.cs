namespace OTPService.Application.Common;

public interface IQueryHandlerAsync<TQuery, TResponse> where TQuery: IQuery<TResponse>
{
    Task<TResponse> Handle(TQuery query);
}