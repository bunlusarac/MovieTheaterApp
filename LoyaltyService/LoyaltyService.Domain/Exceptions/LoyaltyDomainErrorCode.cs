namespace LoyaltyService.Domain.Exceptions;

public enum LoyaltyDomainErrorCode
{
    InsufficientFunds,
    InvalidPointsAmount,
    InvalidDepositOrWithdrawalAmount,
    InvalidDiscountRate,
    InvalidExpirationDate,
    InvalidMaxRedeems,
    CampaignRedeemedMoreThanMaxRedeems,
    CampaignExpired,
}