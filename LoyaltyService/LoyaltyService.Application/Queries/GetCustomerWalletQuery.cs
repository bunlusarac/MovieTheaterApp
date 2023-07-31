using LoyaltyService.Application.DTOs;
using LoyaltyService.Application.Persistence;
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
    private readonly ILoyaltyCustomerRepository _loyaltyCustomerRepository;

    public GetCustomerWalletQueryHandler(ILoyaltyCustomerRepository loyaltyCustomerRepository)
    {
        _loyaltyCustomerRepository = loyaltyCustomerRepository;
    }

    public async Task<WalletDto> Handle(GetCustomerWalletQuery request, CancellationToken cancellationToken)
    {
        var loyaltyCustomer = await _loyaltyCustomerRepository.GetByCustomerId(request.CustomerId);
        var wallet = loyaltyCustomer.Wallet;

        return new WalletDto
        {
            WalletId = wallet.Id,
            LoyaltyCustomerId = wallet.LoyaltyCustomerId,
            PointsBalance = wallet.PointsBalance.Amount
        };
    }
}