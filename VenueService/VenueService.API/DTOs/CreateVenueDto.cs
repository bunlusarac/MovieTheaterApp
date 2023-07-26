using VenueService.Domain.Utils;

namespace VenueService.API.DTOs;

/// <summary>
/// Represents required parameters for venue creation
/// </summary>
public class CreateVenueDto
{
    /// <summary>
    /// Name of the venue to be created, e.g Beyoglu Sinemasi
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Location of the venue to be created as in Turkiye provinces, e.g. Istanbul
    /// </summary>
    public Location Location { get; set; }

    /// <summary>
    /// Represents required parameters for venue creation
    /// </summary>
    /// <param name="name">Name of the venue to be created, e.g Beyoglu Sinemasi</param>
    /// <param name="location">Location of the venue to be created as in Turkiye provinces, e.g. Istanbul</param>
    public CreateVenueDto(string name, Location location)
    {
        Name = name;
        Location = location;
    }
}