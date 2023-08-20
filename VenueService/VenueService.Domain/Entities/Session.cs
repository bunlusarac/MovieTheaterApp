using VenueService.Domain.Common;
using VenueService.Domain.Exceptions;
using VenueService.Domain.Utils;
using VenueService.Domain.ValueObjects;


namespace VenueService.Domain.Entities;

public class Session: EntityBase
{
    public Guid TheaterId { get; set; }
    public Guid MovieId { get; set; }
    public Localization Localization { get; set; }
    public virtual TimeRange TimeRange { get; set; }
    public virtual SeatingState SeatingState { get; set; }
    public virtual List<Pricing> Pricings { get; set; }

    public Session(
        TimeRange timeRange,
        Guid movieId,
        SeatingLayout seatingLayout,
        Localization localization,
        List<Pricing> pricings)
    {
        //TODO: Workaround for not using Add
        //Id = Guid.NewGuid();

        TimeRange = timeRange;
        MovieId = movieId;
        SeatingState = new SeatingState(seatingLayout); 
        Localization = localization;
        Pricings = pricings;
    }

    public Session(TimeRange timeRange, Guid movieId, SeatingLayout layout, Localization localization)
    {
        TimeRange = timeRange;
        MovieId = movieId;
        Localization = localization;
        
        SeatingState = new SeatingState(layout);
        Pricings = new List<Pricing>();
    }

    public void OccupySeat(char rowLetter, int seatNumber, string concurrencyToken)
    {
        if (DateTime.UtcNow > TimeRange.End) throw new VenueDomainException(VenueDomainErrorCode.SessionEnded);
        SeatingState.OccupySeat(rowLetter, seatNumber, concurrencyToken);
    }
    
    public void ReleaseSeat(char rowLetter, int seatNumber/*, string concurrencyToken*/)
    {
        if (DateTime.UtcNow > TimeRange.End) throw new VenueDomainException(VenueDomainErrorCode.SessionEnded);
        SeatingState.ReleaseSeat(rowLetter, seatNumber/*, concurrencyToken*/);
    }

    public Session()
    {
    }
}