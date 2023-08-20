namespace BookingService.Application.DTOs;

public class SessionDto
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
    public string Localization { get; set; }
    
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
    public List<PricingDto> Pricings { get; set; }
    
    /// <summary>
    /// ID of the theater this session belongs to
    /// </summary>
    public Guid TheaterId { get; set; }
    
    /// <summary>
    /// Name of the theater this session belongs to
    /// </summary>
    public string TheaterName { get; set; }
}