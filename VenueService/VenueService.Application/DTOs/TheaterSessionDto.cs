using VenueService.Domain.Entities;
using VenueService.Domain.Utils;

namespace VenueService.Application.DTOs;

/// <summary>
/// Represents a session related to a theater
/// </summary>
public class TheaterSessionDto
{
    /// <summary>
    /// ID of the session
    /// </summary>
    public Guid SessionId { get; set; }
    
    /// <summary>
    /// Start time of the session
    /// </summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>
    /// End time of the session
    /// </summary>
    public DateTime EndTime { get; set; }
    
    /// <summary>
    /// Localization of the session (subtitles, dubbing)
    /// </summary>
    public Localization Localization { get; set; }

    /// <summary>
    /// Remaining capacity of the session
    /// </summary>
    public int Capacity { get; set; }
    
    /// <summary>
    /// ID of the movie that is being presented in the session
    /// </summary>
    public Guid MovieId { get; set; }
    
    /// <summary>
    /// Prices for different ticket types of this session 
    /// </summary>
    public List<Pricing> Pricings { get; set; }

    /// <summary>
    /// Represents a session related to a theater
    /// </summary>
    /// <param name="sessionId"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <param name="localization"></param>
    /// <param name="capacity"></param>
    /// <param name="movieId"></param>
    /// <param name="pricings"></param>
    public TheaterSessionDto(Guid sessionId, DateTime startTime, DateTime endTime, Localization localization, int capacity, Guid movieId, List<Pricing> pricings)
    {
        SessionId = sessionId;
        StartTime = startTime;
        EndTime = endTime;
        Localization = localization;
        Capacity = capacity;
        MovieId = movieId;
        Pricings = pricings;
    }
}