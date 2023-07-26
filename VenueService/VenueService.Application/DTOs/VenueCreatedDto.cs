namespace VenueService.Application.DTOs;

/// <summary>
/// Represents information about the created venue
/// </summary>
public class VenueCreatedDto
{
    /// <summary>
    /// ID of the created venue
    /// </summary>
    public Guid VenueId { get; set; }

    /// <summary>
    /// Represents information about the created venue
    /// </summary>
    /// <param name="venueId">ID of the created venue</param>
    public VenueCreatedDto(Guid venueId)
    {
        VenueId = venueId;
    }
}