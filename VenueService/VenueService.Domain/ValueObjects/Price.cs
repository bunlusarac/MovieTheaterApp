using VenueService.Domain.Common;
using VenueService.Domain.Exceptions;
using VenueService.Domain.Utils;

namespace VenueService.Domain.ValueObjects;

public class Price: ValueObject<Price>
{
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }

    public Price(decimal amount, Currency currency = Currency.TRY)
    {
        if (Amount < 0) throw new VenueDomainException(VenueDomainErrorCode.NegativePriceAmount);
        
        Amount = amount;
        Currency = currency;
    }

    protected override bool EqualsCore(Price other)
    {
        return Currency == other.Currency && Amount == other.Amount;
    }

    protected override int GetHashCodeCore()
    {
        unchecked
        {
            var hashCode = Amount.GetHashCode();
            hashCode = (hashCode * 397) ^ Currency.GetHashCode();
            return hashCode;
        }
    }

    public Price()
    {
    }
}