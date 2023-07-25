using VenueService.Domain.Entities;
using VenueService.Domain.Utils;
using VenueService.Domain.ValueObjects;

namespace VenueService.Application.DTOs;

public class VenueSessionDto
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public Localization Localization { get; set; }
    public int Capacity { get; set; }
    public Guid MovieId { get; set; }
    public List<Pricing> Pricings { get; set; }
    public Guid TheaterId { get; set; }
    public string TheaterName { get; set; }

    public VenueSessionDto(DateTime startTime, DateTime endTime, Localization localization, int capacity, Guid movieId, List<Pricing> pricings, Guid theaterId, string theaterName)
    {
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