using VenueService.Domain.Utils;

namespace VenueService.Application.DTOs;

/// <summary>
/// Represents information about a venue
/// </summary>
public class VenueDto
{
    /// <summary>
    /// ID of the venue
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Name of the venue
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Location of the venue as in Turkiye provinces, e.g Istanbul
    /// </summary>
    public Location Location { get; set; }
    
    /// <summary>
    /// List of theater types contained in this venue, e.g IMAX, 4DX...
    /// </summary>
    public IEnumerable<TheaterType> TheaterTypes { get; set; }

    /// <summary>
    /// Represents information about a venue
    /// </summary>
    /// <param name="id">ID of the venue</param>
    /// <param name="name">Name of the venue</param>
    /// <param name="location">Location of the venue as in Turkiye provinces, e.g Istanbul</param>
    /// <param name="theaterTypes">List of theater types contained in this venue, e.g IMAX, 4DX...</param>
    public VenueDto(Guid id, string name, Location location, IEnumerable<TheaterType> theaterTypes)
    {
        Id = id;
        Name = name;
        Location = location;
        TheaterTypes = theaterTypes;
    }
}