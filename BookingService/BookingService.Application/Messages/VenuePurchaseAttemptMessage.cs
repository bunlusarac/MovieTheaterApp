namespace BookingService.Application.Messages;

public class VenuePurchaseAttemptMessage: IRabbitMessage
{
    public RabbitMessageEvent Event { get; } = RabbitMessageEvent.EVENT_PURCHASE_ATTEMPT_VENUE;
    public Guid VenueId { get; set; }
    public Guid TheaterId { get; set; }
    public Guid SessionId { get; set; }
    
    public char SeatRow { get; set; }
    public int SeatNumber { get; set; }
    public string SeatConcurrencyToken { set; get; }
}