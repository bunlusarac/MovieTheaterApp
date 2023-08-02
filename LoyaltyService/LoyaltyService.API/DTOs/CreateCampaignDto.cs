using LoyaltyService.Domain.Utils;

namespace LoyaltyService.API.DTOs;

public class CreateCampaignDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public CampaignType Type { get; set; }
    public double DiscountRate { get; set; }
    public int MaxRedeems { get; set; }
    public DateTime ExpirationDate { get; set; }
}