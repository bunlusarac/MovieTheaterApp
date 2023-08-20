using BookingService.Domain.Utils;

namespace BookingService.Application.DTOs;

public class PricingDto
{
    public TicketType Type { get; set; }
    public PriceDto Price { get; set; }
}