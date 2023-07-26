namespace VenueService.Application.DTOs;

/// <summary>
/// Represents information about the created session
/// </summary>
public class SessionCreatedDto
{
    /// <summary>
    /// ID of the created session
    /// </summary>
    public Guid SessionId { get; set; }

    /// <summary>
    /// Represents information about the created session
    /// </summary>
    /// <param name="sessionId">ID of the created session</param>
    public SessionCreatedDto(Guid sessionId)
    {
        SessionId = sessionId;
    }
}