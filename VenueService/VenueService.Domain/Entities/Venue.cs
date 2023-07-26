using VenueService.Domain.Common;
using VenueService.Domain.Utils;


namespace VenueService.Domain.Entities;

public class Venue: AggregateRoot
{
    public string Name { get; set; }
    public Location Location { get; set; }
    public virtual List<Theater> Theaters { get; set; }
    
    public Venue(string name, Location location)
    {
        Name = name;
        Location = location;
        Theaters = new List<Theater>();
    }

    public Theater AddTheater(string name, int width, TheaterType type)
    {
        var theater = new Theater(name, width, type);
        Theaters.Add(theater);
        return theater;
    }

    public Venue()
    {
    }
}