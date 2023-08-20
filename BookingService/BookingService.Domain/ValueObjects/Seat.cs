using BookingService.Domain.Common;
using BookingService.Domain.Exceptions;

namespace BookingService.Domain.ValueObjects;

public class Seat: ValueObject<Seat>
{
    public char Row { get; set; }
    public int Number { get; set; }

    public Seat(char row, int number)
    {
        if (row is < 'A' or > 'Z')
            throw new BookingDomainException();

        if (number <= 0)
            throw new BookingDomainException();

        Row = row;
        Number = number;
    }

    protected override bool EqualsCore(Seat other)
    {
        throw new NotImplementedException();
    }

    protected override int GetHashCodeCore()
    {
        throw new NotImplementedException();
    }
}