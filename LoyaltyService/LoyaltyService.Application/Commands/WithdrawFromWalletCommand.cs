using LoyaltyService.Application.Persistence;
using LoyaltyService.Domain.ValueObjects;
using MediatR;

namespace LoyaltyService.Application.Commands;

public class WithdrawFromWalletCommand: IRequest
{
    public Guid CustomerId { get; set; }
    public decimal PointsAmount { get; set; }

    public WithdrawFromWalletCommand(Guid customerId, decimal pointsAmount)
    {
        CustomerId = customerId;
        PointsAmount = pointsAmount;
    }
}

public class WithdrawFromWalletCommandHandler : IRequestHandler<WithdrawFromWalletCommand>
{
    private readonly ILoyaltyCustomerRepository _loyaltyCustomerRepository;

    public WithdrawFromWalletCommandHandler(ILoyaltyCustomerRepository loyaltyCustomerRepository)
    {
        _loyaltyCustomerRepository = loyaltyCustomerRepository;
    }

    public async Task Handle(WithdrawFromWalletCommand request, CancellationToken cancellationToken)
    {
        var loyaltyCustomer = await _loyaltyCustomerRepository.GetByCustomerId(request.CustomerId);
        var wallet = loyaltyCustomer.Wallet;
        
        wallet.Withdraw(new PointsAmount(request.PointsAmount));
    }
}