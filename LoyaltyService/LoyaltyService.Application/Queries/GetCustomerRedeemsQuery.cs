using LoyaltyService.Application.DTOs;
using MediatR;

namespace LoyaltyService.Application.Queries;

public class GetCustomerRedeemsQuery: IRequest<List<RedeemDto>>
{
    public Guid CustomerId { get; set; }

    public GetCustomerRedeemsQuery(Guid customerId)
    {
        CustomerId = customerId;
    }
}

public class GetCustomerRedeemsQueryHandler : IRequestHandler<GetCustomerRedeemsQuery, List<RedeemDto>>
{
    public Task<List<RedeemDto>> Handle(GetCustomerRedeemsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}