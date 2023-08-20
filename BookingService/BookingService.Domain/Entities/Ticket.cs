using BookingService.Domain.Common;
using BookingService.Domain.Utils;
using BookingService.Domain.ValueObjects;

namespace BookingService.Domain.Entities;

public class Ticket: AggregateRoot
{
    public Guid CustomerId { get; set; }
    public Guid SessionId { get; set; }
    public Seat Seat { get; set; }
    public TicketType Type { get; set; }

    public Ticket(Guid customerId, Guid sessionId, Seat seat, TicketType type)
    {
        CustomerId = customerId;
        SessionId = sessionId;
        Seat = seat;
        Type = type;
    }

    public Ticket()
    {
    }
}