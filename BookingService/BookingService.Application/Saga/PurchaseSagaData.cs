namespace BookingService.Application.Saga;

public class PurchaseSagaData
{
    public Guid PurchaseTransactionId;
    public Guid TicketId { get; set; } = new Guid();
    public bool CampaignsRedeemed { get; set; } = false;
    public bool SeatReserved { get; set; } = false;
    public bool PaymentComplete { get; set; } = false;

    public PurchaseSagaData(Guid purchaseTransactionId)
    {
        PurchaseTransactionId = purchaseTransactionId;
    }
}