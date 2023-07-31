using LoyaltyService.Domain.Common;
using LoyaltyService.Domain.Exceptions;
using LoyaltyService.Domain.ValueObjects;

namespace LoyaltyService.Domain.Entities;

public class Wallet: EntityBase
{
    public Guid LoyaltyCustomerId { get; set; }
    public Guid CustomerId { get; set; }
    public PointsAmount PointsBalance { get; set; }

    public Wallet(Guid loyaltyCustomerId, Guid customerId)
    {
        LoyaltyCustomerId = loyaltyCustomerId;
        CustomerId = customerId;
        PointsBalance = new PointsAmount();
    }

    public Wallet(Guid loyaltyCustomerId, Guid customerId, PointsAmount pointsBalance)
    {
        LoyaltyCustomerId = loyaltyCustomerId;
        CustomerId = customerId;
        PointsBalance = pointsBalance;
    }

    public Wallet(Guid loyaltyCustomerId, Guid customerId, decimal pointsBalance)
    {
        LoyaltyCustomerId = loyaltyCustomerId;
        CustomerId = customerId;
        PointsBalance = new PointsAmount(pointsBalance);
    }

    /// <summary>
    /// Deposit arbitrary amount of points to this wallet. 
    /// </summary>
    /// <param name="depositAmount">amount of points to deposit</param>
    public void Deposit(PointsAmount depositAmount)
    {
        PointsBalance += depositAmount;
    }

    /// <summary>
    /// Withdraw arbitrary amount of points from this wallet.
    /// </summary>
    /// <param name="withdrawalAmount">amount of points to withdraw</param>
    /// <exception cref="LoyaltyDomainException"></exception>
    public void Withdraw(PointsAmount withdrawalAmount)
    {
        if (withdrawalAmount < PointsBalance)
        {
            throw new LoyaltyDomainException(LoyaltyDomainErrorCode.InsufficientFunds);
        }

        PointsBalance -= withdrawalAmount;
    }
}