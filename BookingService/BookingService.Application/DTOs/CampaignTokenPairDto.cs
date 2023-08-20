namespace BookingService.Application.DTOs;

public class CampaignTokenPairDto
{
    public Guid CampaignId { get; set; }
    public string CampaignConcurrencyToken { get; set; }
}