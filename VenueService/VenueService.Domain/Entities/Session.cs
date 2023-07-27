using VenueService.Domain.Common;
using VenueService.Domain.Exceptions;
using VenueService.Domain.Utils;
using VenueService.Domain.ValueObjects;


namespace VenueService.Domain.Entities;

public class Session: EntityBase
{
    public virtual TimeRange TimeRange { get; set; }
    public Guid MovieId { get; set; }
    public virtual SeatingState SeatingState { get; set; }
    public Localization Localization { get; set; }
    public virtual List<Pricing> Pricings { get; set; }

    public Session(
        TimeRange timeRange,
        Guid movieId,
        SeatingLayout seatingLayout,
        Localization localization,
        List<Pricing> pricings)
    {
        TimeRange = timeRange;
        MovieId = movieId;
        SeatingState = new SeatingState(seatingLayout);
        Localization = localization;
        Pricings = pricings;
    }
    
    public void OccupySeat(char rowLetter, int seatNumber)
    {
        if (DateTime.UtcNow > TimeRange.End) throw new VenueDomainException(VenueDomainErrorCode.SessionEnded);
        SeatingState.OccupySeat(rowLetter, seatNumber);
    }
    
    public void ReleaseSeat(char rowLetter, int seatNumber)
    {
        if (DateTime.UtcNow > TimeRange.End) throw new VenueDomainException(VenueDomainErrorCode.SessionEnded);
        SeatingState.ReleaseSeat(rowLetter, seatNumber);
    }

    public Session()
    {
    }
}