using VenueService.Domain.Utils;

namespace VenueService.Application.DTOs;

public class VenueDto
{
    public string Name;
    public Location Location;
    public IEnumerable<TheaterType> TheaterTypes;
}