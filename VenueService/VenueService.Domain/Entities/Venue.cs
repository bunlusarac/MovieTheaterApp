using VenueService.Domain.Common;
using VenueService.Domain.Utils;

namespace VenueService.Domain.Entities;

public class Venue: AggregateRoot
{
    public string Name;
    public Location Location;
    public IList<Session> Sessions;

    public Venue(string name, Location location)
    {
        Name = name;
        Location = location;
        Sessions = new List<Session>();
    }
}