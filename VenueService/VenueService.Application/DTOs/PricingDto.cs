using VenueService.Domain.Utils;

namespace VenueService.Application.DTOs;

public class PricingDto
{
    public TicketType Type { get; set; }
    public Currency Currency { get; set; }
    public decimal Amount { get; set; }
}