using VenueService.Domain.Utils;

namespace VenueService.Application.DTOs;

public class VenueDto
{
    public string Name;
    public Location Location;
    public IEnumerable<TheaterType> TheaterTypes;

    public VenueDto(string name, Location location, IEnumerable<TheaterType> theaterTypes)
    {
        Name = name;
        Location = location;
        TheaterTypes = theaterTypes;
    }
}