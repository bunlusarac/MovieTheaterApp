using LoyaltyService.Domain.Utils;
using LoyaltyService.Domain.ValueObjects;

namespace LoyaltyService.Application.DTOs;

public class CampaignWithVersioningDto
{
    public Guid CampaignId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public CampaignType Type { get; set; }
    public double DiscountRate { get; set; }
    public int MaxRedeems { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string Version { get; set; }
}