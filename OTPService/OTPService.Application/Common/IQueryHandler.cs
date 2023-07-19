namespace OTPService.Application.Common;

public interface IQueryHandler<TQuery, TResponse> where TQuery: IQuery<TResponse>
{
    TResponse Handle(TQuery query);
}