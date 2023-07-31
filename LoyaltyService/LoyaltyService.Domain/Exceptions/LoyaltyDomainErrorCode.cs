namespace LoyaltyService.Domain.Exceptions;

public enum LoyaltyDomainErrorCode
{
    InsufficientFunds,
    InvalidPointsAmount,
    InvalidDiscountRate,
    InvalidExpirationDate,
    InvalidMaxRedeems,
    CampaignRedeemedMoreThanMaxRedeems,
    CampaignExpired,
}