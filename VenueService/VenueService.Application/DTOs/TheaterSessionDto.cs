using VenueService.Domain.Entities;
using VenueService.Domain.Utils;

namespace VenueService.Application.DTOs;

public class TheaterSessionDto
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public Localization Localization { get; set; }
    public int Capacity { get; set; }
    public Guid MovieId { get; set; }
    public List<Pricing> Pricings { get; set; }

    public TheaterSessionDto(DateTime startTime, DateTime endTime, Localization localization, int capacity, Guid movieId,
        List<Pricing> pricings)
    {
        StartTime = startTime;
        EndTime = endTime;
        Localization = localization;
        Capacity = capacity;
        MovieId = movieId;
        Pricings = pricings;
    }
}