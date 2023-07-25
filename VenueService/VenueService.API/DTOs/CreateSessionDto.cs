using System.Text.Json.Serialization;
using VenueService.API.Utils;
using VenueService.Domain.Entities;
using VenueService.Domain.Utils;
using VenueService.Domain.ValueObjects;

namespace VenueService.API.DTOs;

public class CreateSessionDto
{
    public Guid MovieId { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
    
    public Localization Localization { get; set; }
    public List<Pricing> Pricings { get; set; }

    public CreateSessionDto(Guid movieId, DateTime startTime, DateTime endTime, Localization localization, List<Pricing> pricings)
    {
        MovieId = movieId;
        StartTime = startTime;
        EndTime = endTime;
        Localization = localization;
        Pricings = pricings;
    }
}