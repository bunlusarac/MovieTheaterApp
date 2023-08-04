using BookingService.Domain.Common;

namespace BookingService.Domain.Entities;

public class Ticket: AggregateRoot
{
    public Guid CustomerId { get; set; }
    public Guid SessionId { get; set; }

    public Ticket(Guid customerId, Guid sessionId)
    {
        CustomerId = customerId;
        SessionId = sessionId;
    }
}