using LoyaltyService.Domain.Common;
using LoyaltyService.Domain.ValueObjects;

namespace LoyaltyService.Domain.Entities;

public class Redeem: EntityBase
{
    public Guid CampaignId { get; set; }
    public Guid LoyaltyCustomerId { get; set; }
    public DateTime RedeemDate { get; set; }
    public PointsAmount Transaction { get; set; }

    public Redeem(Guid campaignId, Guid loyaltyCustomerId, DateTime redeemDate, PointsAmount transaction)
    {
        CampaignId = campaignId;
        LoyaltyCustomerId = loyaltyCustomerId;
        RedeemDate = redeemDate;
        Transaction = transaction;
    }
}