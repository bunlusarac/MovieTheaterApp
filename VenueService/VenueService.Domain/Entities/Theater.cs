using VenueService.Domain.Common;
using VenueService.Domain.Exceptions;
using VenueService.Domain.Utils;
using VenueService.Domain.ValueObjects;


namespace VenueService.Domain.Entities;

public class Theater: EntityBase
{
    public virtual SeatingLayout Layout { get; set; }
    public virtual List<Session> Sessions { get; set; }
    public TheaterType Type { get; set; }
    public string Name { get; set; }

    
    public Theater(string name, int width, TheaterType type = TheaterType.Standard2D)
    {
        Layout = new SeatingLayout(width);
        Sessions = new List<Session>();
        Type = type;
        Name = name;
    }

    public Theater(string name, SeatingLayout layout, TheaterType type = TheaterType.Standard2D)
    {
        Layout = layout;
        Sessions = new List<Session>();
        Type = type;
        Name = name;
    }

    public Session AddSession(
        TimeRange timeRange,
        Guid movieId,
        Localization localization)
    {
        if(Sessions.Any(s => s.TimeRange.OverlapsWith(timeRange))) 
            throw new VenueDomainException(VenueDomainErrorCode.SessionTimeRangeOverlap);

        var session = new Session(timeRange, movieId, Layout, localization);
        Sessions.Add(session);

        return session;
    }
    
    public Session AddSession(
        TimeRange timeRange, 
        Guid movieId,
        Localization localization,
        List<Pricing> pricings)
    {
        if(Sessions.Any(s => s.TimeRange.OverlapsWith(timeRange))) 
            throw new VenueDomainException(VenueDomainErrorCode.SessionTimeRangeOverlap);
        
        var session = new Session(timeRange, movieId, Layout, localization, pricings);
        //var session = new Session();
        
        //TODO
        //session.Id = Guid.NewGuid();
        
        Sessions.Add(session);

        return session;
    }

    public void DeleteSession(Guid sessionId)
    {
        var session = Sessions.FirstOrDefault(s => s.Id == sessionId);
        if (session == null) throw new VenueDomainException(VenueDomainErrorCode.SessionDoesNotExist);
        
        session.SeatingState.StateSeats.Clear();
        session.Pricings.Clear();
        Sessions.Remove(session);
    }

    public Theater() { }
}