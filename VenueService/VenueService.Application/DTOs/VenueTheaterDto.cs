using VenueService.Domain.Utils;

namespace VenueService.Application.DTOs;

/// <summary>
/// Represents a theater related to a venue
/// </summary>
public class VenueTheaterDto
{
    /// <summary>
    /// ID of the theater
    /// </summary>
    public Guid TheaterId { get; set; }
    
    /// <summary>
    /// Name of the theater
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Type of the theater (Standard 2D, IMAX...)
    /// </summary>
    public TheaterType Type { get; set; }

    /// <summary>
    /// Represents a theater related to a venue
    /// </summary>
    /// <param name="theaterId">ID of the theater</param>
    /// <param name="name">Name of the theater</param>
    /// <param name="type">Type of the theater (Standard 2D, IMAX...)</param>
    public VenueTheaterDto(Guid theaterId, string name, TheaterType type)
    {
        TheaterId = theaterId;
        Name = name;
        Type = type;
    }
}