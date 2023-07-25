using VenueService.Domain.Utils;

namespace VenueService.Application.DTOs;

public class VenueDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Location Location { get; set; }
    public IEnumerable<TheaterType> TheaterTypes { get; set; }

    public VenueDto(Guid id, string name, Location location, IEnumerable<TheaterType> theaterTypes)
    {
        Id = id;
        Name = name;
        Location = location;
        TheaterTypes = theaterTypes;
    }
}