using LoyaltyService.Domain.Common;
using LoyaltyService.Domain.Exceptions;
using LoyaltyService.Domain.ValueObjects;

namespace LoyaltyService.Domain.Entities;

public class Wallet: EntityBase
{
    public Guid LoyaltyCustomerId { get; set; }
    public PointsAmount PointsBalance { get; set; }

    public Wallet(Guid loyaltyCustomerId, Guid customerId)
    {
        LoyaltyCustomerId = loyaltyCustomerId;
        PointsBalance = new PointsAmount();
    }

    public Wallet(Guid loyaltyCustomerId, Guid customerId, PointsAmount pointsBalance)
    {
        LoyaltyCustomerId = loyaltyCustomerId;
        PointsBalance = pointsBalance;
    }

    public Wallet(Guid loyaltyCustomerId, Guid customerId, decimal pointsBalance)
    {
        LoyaltyCustomerId = loyaltyCustomerId;
        PointsBalance = new PointsAmount(pointsBalance);
    }

    /// <summary>
    /// Deposit arbitrary amount of points to this wallet. 
    /// </summary>
    /// <param name="depositAmount">amount of points to deposit</param>
    /// <exception cref="LoyaltyDomainException">Thrown when amount to withdraw is non-negative.</exception>
    public void Deposit(PointsAmount depositAmount)
    {
        if (depositAmount <= 0)
            throw new LoyaltyDomainException(LoyaltyDomainErrorCode.InvalidDepositOrWithdrawalAmount);

        PointsBalance += depositAmount;
    }

    /// <summary>
    /// Withdraw arbitrary amount of points from this wallet.
    /// </summary>
    /// <param name="withdrawalAmount">amount of points to withdraw</param>
    /// <exception cref="LoyaltyDomainException">Thrown when amount to withdraw is non-negative
    /// or there is insufficient funds in the wallet.</exception>
    public void Withdraw(PointsAmount withdrawalAmount)
    {
        if (withdrawalAmount <= 0)
            throw new LoyaltyDomainException(LoyaltyDomainErrorCode.InvalidDepositOrWithdrawalAmount);
        
        if (withdrawalAmount > PointsBalance) 
            throw new LoyaltyDomainException(LoyaltyDomainErrorCode.InsufficientFunds);

        PointsBalance -= withdrawalAmount;
    }

    public Wallet()
    {
    }
}