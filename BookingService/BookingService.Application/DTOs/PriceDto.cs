using BookingService.Domain.Utils;

namespace BookingService.Application.DTOs;

public class PriceDto
{
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
}