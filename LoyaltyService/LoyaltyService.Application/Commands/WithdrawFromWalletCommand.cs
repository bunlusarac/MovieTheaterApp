using LoyaltyService.Domain.ValueObjects;
using MediatR;

namespace LoyaltyService.Application.Commands;

public class WithdrawFromWalletCommand: IRequest
{
    public Guid CustomerId { get; set; }
    public PointsAmount PointsAmount { get; set; }

    public WithdrawFromWalletCommand(Guid customerId, PointsAmount pointsAmount)
    {
        CustomerId = customerId;
        PointsAmount = pointsAmount;
    }
}

public class WithdrawFromWalletCommandHandler : IRequestHandler<WithdrawFromWalletCommand>
{
    public Task Handle(WithdrawFromWalletCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}