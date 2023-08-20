using BookingService.Domain.Utils;
using BookingService.Domain.ValueObjects;

namespace BookingService.Application.DTOs;

public class CustomerTicketDto
{
    public Guid TicketId { get; set; }
    public Guid SessionId { get; set; }
    public char SeatRow { get; set; }
    public int SeatNumber { get; set; }
    public TicketType Type { get; set; }
}