using BookingService.Application.DTOs;

namespace BookingService.Application.Messages;

public class LoyaltyPurchaseAttemptMessage: IRabbitMessage
{
    public RabbitMessageEvent Event { get; } = RabbitMessageEvent.EVENT_PURCHASE_ATTEMPT_LOYALTY;
    public Guid CustomerId { set; get; }
    public Guid CampaignId { set; get; }
    public string CampaignConcurrencyToken { set; get; }
}