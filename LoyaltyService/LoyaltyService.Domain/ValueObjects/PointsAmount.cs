using LoyaltyService.Domain.Common;
using LoyaltyService.Domain.Exceptions;

namespace LoyaltyService.Domain.ValueObjects;

public class PointsAmount: ValueObject<PointsAmount>
{
    public decimal Amount { get; set; }
    
    protected override bool EqualsCore(PointsAmount other)
    {
        return other.Amount == Amount;
    }

    protected override int GetHashCodeCore()
    {
        return Amount.GetHashCode();
    }

    public PointsAmount(decimal amount)
    {
        if (amount < 0) throw new LoyaltyDomainException(LoyaltyDomainErrorCode.InvalidPointsAmount);
        
        Amount = amount;
    }

    public PointsAmount()
    {
        Amount = decimal.Zero;
    }

    public static PointsAmount operator +(PointsAmount left, PointsAmount right)
    {
        return new PointsAmount(left.Amount + right.Amount);
    }

    public static PointsAmount operator *(PointsAmount left, decimal right)
    {
        return new PointsAmount(left.Amount * right);
    }
    
    public static PointsAmount operator *(decimal left, PointsAmount right)
    {
        return right * left;
    }

    public static PointsAmount operator *(PointsAmount left, PointsAmount right)
    {
        return new PointsAmount(left.Amount * right.Amount);
    }

    public static PointsAmount operator -(PointsAmount left, PointsAmount right)
    {
        return new PointsAmount(left.Amount + decimal.MinusOne * right.Amount);
    }
    
    public static PointsAmount operator /(PointsAmount left, decimal right)
    {
        return new PointsAmount(left.Amount / right);
    }
    
    public static PointsAmount operator /(PointsAmount left, PointsAmount right)
    {
        return new PointsAmount(left.Amount / right.Amount);
    }
    
    public static bool operator <=(decimal left, PointsAmount right)
    {
        return left <= right.Amount;
    }
    
    public static bool operator >=(decimal left, PointsAmount right)
    {
        return left >= right.Amount;
    }
    
    public static bool operator <=(PointsAmount left, decimal right)
    {
        return left.Amount <= right;
    }
    
    public static bool operator >=(PointsAmount left, decimal right)
    {
        return left.Amount >= right;
    }
    
    public static bool operator <(PointsAmount left, PointsAmount right)
    {
        return left.Amount < right.Amount;
    }
    
    public static bool operator >(PointsAmount left, PointsAmount right)
    {
        return left.Amount > right.Amount;
    }
    
    public static bool operator <=(PointsAmount left, PointsAmount right)
    {
        return left.Amount <= right.Amount;
    }
    
    public static bool operator >=(PointsAmount left, PointsAmount right)
    {
        return left.Amount >= right.Amount;
    }
    
    public static bool operator ==(PointsAmount left, PointsAmount right)
    {
        return left.Amount == right.Amount;
    }
    
    public static bool operator !=(PointsAmount left, PointsAmount right)
    {
        return left.Amount != right.Amount;
    }
}