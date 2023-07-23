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
    public Dictionary<TicketType, Price> Pricing;
}