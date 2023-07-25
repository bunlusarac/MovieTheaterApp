using VenueService.Domain.Common;
using VenueService.Domain.Utils;

namespace VenueService.Domain.Entities;

public class Venue: AggregateRoot
{
    public string Name;
    public Location Location;
    //public IList<Session> Sessions;
    public virtual List<Theater> Theaters { get; set; }
    
    public Venue(string name, Location location)
    {
        Name = name;
        Location = location;
        Theaters = new List<Theater>();
    }

    public void AddTheater(string name, int width, TheaterType type)
    {
        Theaters.Add(new Theater(name, width, type));
    }
    
    public void AddTheater(string name, SeatingLayout layout, TheaterType type)
    {
        Theaters.Add(new Theater(name, layout, type));
    }

    public void AddTheater(Theater theater)
    {
        Theaters.Add(theater);
    }
    
    public Venue()
    {
    }
}