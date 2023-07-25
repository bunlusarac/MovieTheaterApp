using VenueService.Domain.Utils;

namespace VenueService.API.DTOs;

public class CreateVenueDto
{
    public string Name { get; set; }
    public Location Location { get; set; }

    public CreateVenueDto(string name, Location location)
    {
        Name = name;
        Location = location;
    }
}