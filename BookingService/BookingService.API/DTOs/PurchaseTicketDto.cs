using BookingService.Domain.Utils;

namespace BookingService.API.DTOs;

public class PurchaseTicketDto
{
    public Guid VenueId { get; set; }
    public Guid TheaterId { get; set; }
    public Guid SessionId { get; set; }
    
    public char SeatRow { get; set; }
    public int SeatNumber { get; set; }
    public string SeatConcurrencyToken { set; get; }
    
    public TicketType TicketType { get; set; }

    public Guid CampaignId { get; set; }
    public string CampaignConcurrencyToken { get; set; }
}