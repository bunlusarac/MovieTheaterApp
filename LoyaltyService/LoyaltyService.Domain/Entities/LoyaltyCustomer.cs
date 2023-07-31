using LoyaltyService.Domain.Common;
using LoyaltyService.Domain.Exceptions;
using LoyaltyService.Domain.ValueObjects;

namespace LoyaltyService.Domain.Entities;

public class LoyaltyCustomer: AggregateRoot
{
    public Guid CustomerId { get; set; }
    public Wallet Wallet { get; set; }
    public List<Redeem> Redeems { get; set; }

    public LoyaltyCustomer(Guid customerId)
    {
        CustomerId = customerId;
        Wallet = new Wallet(Id, CustomerId);
        Redeems = new List<Redeem>();
    }

    public void RedeemCampaign(Campaign campaign)
    {
        if (campaign.ExpirationDate <= DateTime.UtcNow)
            throw new LoyaltyDomainException(LoyaltyDomainErrorCode.CampaignExpired);
        
        var previousRedeems = Redeems.Where(r => r.CampaignId == campaign.Id);

        if (previousRedeems.Count() >= campaign.MaxRedeems)
            throw new LoyaltyDomainException(LoyaltyDomainErrorCode.CampaignRedeemedMoreThanMaxRedeems);

        Wallet.Withdraw(campaign.Cost);
        
        var redeem = new Redeem(campaign.Id, Id, DateTime.UtcNow, campaign.Cost);
        Redeems.Add(redeem);
    }
}