using LoyaltyService.Application.DTOs;
using LoyaltyService.Application.Persistence;
using LoyaltyService.Domain.ValueObjects;
using MediatR;

namespace LoyaltyService.Application.Commands;

public class DepositToWalletCommand: IRequest
{
    public Guid CustomerId { get; set; }
    public decimal PointsAmount { get; set; }

    public DepositToWalletCommand(Guid customerId, decimal pointsAmount)
    {
        CustomerId = customerId;
        PointsAmount = pointsAmount;
    }
}

public class DepositToWalletCommandHandler : IRequestHandler<DepositToWalletCommand>
{
    private readonly ILoyaltyCustomerRepository _loyaltyCustomerRepository;

    public DepositToWalletCommandHandler(ILoyaltyCustomerRepository loyaltyCustomerRepository)
    {
        _loyaltyCustomerRepository = loyaltyCustomerRepository;
    }

    public async Task Handle(DepositToWalletCommand request, CancellationToken cancellationToken)
    {
        var loyaltyCustomer = await _loyaltyCustomerRepository.GetByCustomerId(request.CustomerId);
        var wallet = loyaltyCustomer.Wallet;
        
        wallet.Deposit(new PointsAmount(request.PointsAmount));
        await _loyaltyCustomerRepository.Update(loyaltyCustomer);
    }
}