using System.Text.Json.Serialization;
using VenueService.Application.DTOs;
using VenueService.Domain.Entities;
using VenueService.Domain.Utils;
using VenueService.Domain.ValueObjects;

namespace VenueService.API.DTOs;

/// <summary>
/// Represents required parameters for session creation
/// </summary>
public class CreateSessionDto
{
    /// <summary>
    /// ID of the movie that will be presented in the session
    /// </summary>
    public Guid MovieId { get; set; }
    
    /// <summary>
    /// Start time of the session
    /// </summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>
    /// End time of the session
    /// </summary>
    public DateTime EndTime { get; set; }
    
    /// <summary>
    /// Localization for this session (subtitles, dubbing)
    /// </summary>
    public Localization Localization { get; set; }
    
    /// <summary>
    /// Prices for different ticket types of this session 
    /// </summary>
    public List<PricingDto> Pricings { get; set; }
    
    /// <summary>
    /// Represents required parameters for session creation
    /// </summary>
    /// <param name="movieId">ID of the movie that will be presented in the session</param>
    /// <param name="startTime">Start time of the session</param>
    /// <param name="endTime">End time of the session</param>
    /// <param name="localization">Localization for this session (subtitles, dubbing)</param>
    /// <param name="pricings">Prices for different ticket types of this session</param>
    public CreateSessionDto(Guid movieId, DateTime startTime, DateTime endTime, Localization localization, List<PricingDto> pricings)
    {
        MovieId = movieId;
        StartTime = startTime;
        EndTime = endTime;
        Localization = localization;
        Pricings = pricings;
    }
}