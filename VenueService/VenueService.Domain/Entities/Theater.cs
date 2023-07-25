using VenueService.Domain.Common;
using VenueService.Domain.Utils;
using VenueService.Domain.ValueObjects;

namespace VenueService.Domain.Entities;

public class Theater: EntityBase
{
    public virtual SeatingLayout Layout { get; set; }
    public virtual List<Session> Sessions { get; set; }
    public TheaterType Type;
    public string Name;
    
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
        Localization localization,
        List<Pricing> pricing)
    {
        if(Sessions.Any(s => s.TimeRange.OverlapsWith(timeRange))) 
            throw new Exception();
        
        var session = new Session(timeRange, movieId, Layout, localization, pricing);
        Sessions.Add(session);

        return session;
    }
    
    public void AddSession(Session session)
    {
        Sessions.Add(session);
    }
    
    public void DeleteSession(Session session)
    {
        Sessions.Remove(session);
    }
    
    public void DeleteSession(Guid sessionId)
    {
        Sessions.Remove(Sessions.First(s => s.Id == sessionId));
    }

    
    public Theater()
    {
    }
}