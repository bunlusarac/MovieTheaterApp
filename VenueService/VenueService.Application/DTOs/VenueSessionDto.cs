using VenueService.Domain.Entities;
using VenueService.Domain.Utils;
using VenueService.Domain.ValueObjects;

namespace VenueService.Application.DTOs;

/// <summary>
/// Represents a session related to a venue
/// </summary>
public class VenueSessionDto
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
    /// ID of the theater this session belongs to
    /// </summary>
    public Guid TheaterId { get; set; }
    
    /// <summary>
    /// Name of the theater this session belongs to
    /// </summary>
    public string TheaterName { get; set; }

    /// <summary>
    /// Represents a session related to a venue
    /// </summary>
    /// <param name="sessionId">ID of the session</param>
    /// <param name="startTime">Start time of the session</param>
    /// <param name="endTime">End time of the session</param>
    /// <param name="localization">Localization of the session (subtitles, dubbing)</param>
    /// <param name="capacity">Remaining capacity of the session</param>
    /// <param name="movieId">ID of the movie that is being presented in the session</param>
    /// <param name="pricings">Prices for different ticket types of this session </param>
    /// <param name="theaterId">ID of the theater this session belongs to</param>
    /// <param name="theaterName">Name of the theater this session belongs to</param>
    public VenueSessionDto(Guid sessionId, DateTime startTime, DateTime endTime, Localization localization, int capacity, Guid movieId, List<Pricing> pricings, Guid theaterId, string theaterName)
    {
        SessionId = sessionId;
        StartTime = startTime;
        EndTime = endTime;
        Localization = localization;
        Capacity = capacity;
        MovieId = movieId;
        Pricings = pricings;
        TheaterId = theaterId;
        TheaterName = theaterName;
    }
}