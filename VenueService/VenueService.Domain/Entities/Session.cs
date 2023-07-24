using VenueService.Domain.Common;
using VenueService.Domain.Utils;
using VenueService.Domain.ValueObjects;

namespace VenueService.Domain.Entities;

public class Session: EntityBase
{
    public TimeRange TimeRange;
    public Guid MovieId;
    public SeatingState SeatingState;
    public Localization Localization;
    public List<Pricing> Pricings;
    
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
        SeatingState.OccupySeat(rowLetter, seatNumber);
    }
    
    public void ReleaseSeat(char rowLetter, int seatNumber)
    {
        SeatingState.ReleaseSeat(rowLetter, seatNumber);
    }

    public Session()
    {
    }
}