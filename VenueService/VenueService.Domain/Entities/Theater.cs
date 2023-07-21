using VenueService.Domain.Common;
using VenueService.Domain.Utils;
using VenueService.Domain.ValueObjects;

namespace VenueService.Domain.Entities;

public class Theater: EntityBase
{
    public SeatingLayout Layout;
    public List<Session> Sessions;
    public TheaterType Type;
    
    public Theater(int width, TheaterType type = TheaterType.Standard2D)
    {
        Layout = new SeatingLayout(width);
        Sessions = new List<Session>();
        Type = type;
    }

    public Session AddSession(
        TimeRange timeRange, 
        Guid movieId, 
        SeatingLayout seatingLayout, 
        Localization localization,
        Pricing pricing)
    {
        if(Sessions.Any(s => s.TimeRange.OverlapsWith(timeRange))) 
            throw new Exception();
        
        var session = new Session(timeRange, movieId, seatingLayout, localization, pricing);
        Sessions.Add(session);

        return session;
    }
    
    public void AddSession(Session session)
    {
        Sessions.Add(session);
    }
}