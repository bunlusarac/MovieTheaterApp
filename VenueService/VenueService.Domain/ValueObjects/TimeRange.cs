using VenueService.Domain.Common;

namespace VenueService.Domain.ValueObjects;

public class TimeRange: ValueObject<TimeRange>
{
    public DateTime Start;
    public DateTime End;

    public TimeRange(DateTime start, DateTime end)
    {
        Start  = start;
        End = end;
    }

    public bool OverlapsWith(TimeRange other)
    {
        return
            other.Start <= Start && other.End <= End ||
            other.Start >= Start && other.End >= End ||
            other.Start <= Start && other.End >= End ||
            other.Start >= Start && other.End <= End;
    }
    
    protected override bool EqualsCore(TimeRange other)
    {
        return Start == other.Start && End == other.End;
    }

    protected override int GetHashCodeCore()
    {
        unchecked
        {
            var hashCode = Start.GetHashCode();
            hashCode = hashCode * 397 ^ End.GetHashCode();
            return hashCode;
        }
    }

    public TimeRange()
    {
    }
}