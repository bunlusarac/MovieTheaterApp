using VenueService.Domain.Entities;
using VenueService.Domain.Utils;
using VenueService.Domain.ValueObjects;

namespace VenueService.Application.DTOs;

public class VenueSessionDto
{
    public DateTime StartTime;
    public DateTime EndTime;
    public Localization Localization;
    public int Capacity;
    public Guid MovieId;
    public List<Pricing> Pricings;

    public VenueSessionDto(DateTime startTime, DateTime endTime, Localization localization, int capacity, Guid movieId,
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