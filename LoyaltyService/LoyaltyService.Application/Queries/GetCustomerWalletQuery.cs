using LoyaltyService.Application.DTOs;
using MediatR;

namespace LoyaltyService.Application.Queries;

public class GetCustomerWalletQuery: IRequest<WalletDto>
{
    public Guid CustomerId { get; set; }

    public GetCustomerWalletQuery(Guid customerId)
    {
        CustomerId = customerId;
    }
}

public class GetCustomerWalletQueryHandler : IRequestHandler<GetCustomerWalletQuery, WalletDto>
{
    public Task<WalletDto> Handle(GetCustomerWalletQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}