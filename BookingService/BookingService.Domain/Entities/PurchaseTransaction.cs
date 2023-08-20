using BookingService.Domain.Common;

namespace BookingService.Domain.Entities;

public class PurchaseTransaction: AggregateRoot
{
    public bool CampaignsRedeemed { get; set; } = false;
    public bool SeatReserved { get; set; } = false;
    public bool PaymentComplete { get; set; } = false;

    public void RedeemCampaign() => CampaignsRedeemed = true;
    public void ReserveSeat() => SeatReserved = true;
    public void CompletePayment() => PaymentComplete = true;
}