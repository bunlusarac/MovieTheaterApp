using LoyaltyService.Domain.ValueObjects;

namespace LoyaltyService.Application.DTOs;

public class RedeemDto
{
    public Guid CampaignId { get; set; }
    public Guid LoyaltyCustomerId { get; set; }
    public DateTime RedeemDate { get; set; }
    public PointsAmount Transaction { get; set; }
}