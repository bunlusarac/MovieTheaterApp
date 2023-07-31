using LoyaltyService.Domain.Common;
using LoyaltyService.Domain.Exceptions;
using LoyaltyService.Domain.Utils;
using LoyaltyService.Domain.ValueObjects;

namespace LoyaltyService.Domain.Entities;

public class Campaign: AggregateRoot
{
    public string Name { get; set; }
    public string Description { get; set; }
    public PointsAmount Cost { get; set; }
    public CampaignType Type { get; set; }
    public double DiscountRate { get; set; }
    public int MaxRedeems { get; set; }
    public DateTime ExpirationDate { get; set; }

    public Campaign(string name, string description, decimal cost, CampaignType type, double discountRate, int maxRedeems, DateTime expirationDate)
    {
        AssignCampaignDetails(name, description, cost, type, discountRate, maxRedeems, expirationDate);
    }
    
    public void AssignCampaignDetails(string name, string description, decimal cost, CampaignType type, double discountRate,
        int maxRedeems, DateTime expirationDate)
    {
        ValidateCampaignDetails(discountRate, maxRedeems, expirationDate);
        
        Name = name;
        Description = description;
        Cost = new PointsAmount(cost);
        Type = type;
        DiscountRate = discountRate;
        MaxRedeems = maxRedeems;
        ExpirationDate = expirationDate;
    }
    
    private void ValidateCampaignDetails(double discountRate, int maxRedeems, DateTime expirationDate)
    {
        if (discountRate <= 0) 
            throw new LoyaltyDomainException(LoyaltyDomainErrorCode.InvalidDiscountRate);
        
        if (maxRedeems <= 0) 
            throw new LoyaltyDomainException(LoyaltyDomainErrorCode.InvalidMaxRedeems);
        
        if (expirationDate >= DateTime.UtcNow)
            throw new LoyaltyDomainException(LoyaltyDomainErrorCode.InvalidExpirationDate);
    }

}