using LoyaltyService.Application.DTOs;
using LoyaltyService.Domain.ValueObjects;
using MediatR;

namespace LoyaltyService.Application.Commands;

public class DepositToWalletCommand: IRequest
{
    public Guid CustomerId { get; set; }
    public PointsAmount PointsAmount { get; set; }

    public DepositToWalletCommand(Guid customerId, PointsAmount pointsAmount)
    {
        CustomerId = customerId;
        PointsAmount = pointsAmount;
    }
}

public class DepositToWalletCommandHandler : IRequestHandler<DepositToWalletCommand>
{
    public Task Handle(DepositToWalletCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}