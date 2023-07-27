namespace VenueService.Application.DTOs;

/// <summary>
/// Represents information about the created theater
/// </summary>
public class TheaterCreatedDto
{
    /// <summary>
    /// ID of the created theater
    /// </summary>
    public Guid TheaterId { get; set; }

    /// <summary>
    /// Represents information about the created theater
    /// </summary>
    /// <param name="theaterId">ID of the created theater</param>
    public TheaterCreatedDto(Guid theaterId)
    {
        TheaterId = theaterId;
    }
}