namespace LoyaltyService.Domain.Exceptions;

public class LoyaltyDomainException: Exception
{
    public LoyaltyProblemDetails ProblemDetails;
    
    public LoyaltyDomainException(LoyaltyDomainErrorCode domainErrorCode)
    {
        ProblemDetails = GetProblemDetailsFromErrorCode(domainErrorCode);
    }
    
    public LoyaltyDomainException(LoyaltyDomainErrorCode domainErrorCode, Exception innerException) :
        base(GetProblemDetailsFromErrorCode(domainErrorCode).Detail, innerException)
    {
        ProblemDetails = GetProblemDetailsFromErrorCode(domainErrorCode);
    }
    
    private static LoyaltyProblemDetails GetProblemDetailsFromErrorCode(LoyaltyDomainErrorCode domainErrorCode)
    {
        return domainErrorCode switch
        {
            LoyaltyDomainErrorCode.CampaignExpired => new LoyaltyProblemDetails
            {
                Type = "https://docs.loyalty.com/errors/campaign-expired", 
                Title = "Campaign expired",
                Status = 400,
                Detail = "This campaign has expired thus it can no longer be redeemed."
            },
            LoyaltyDomainErrorCode.InsufficientFunds => new LoyaltyProblemDetails
            {
                Type = "https://docs.loyalty.com/errors/insufficient-funds",
                Title = "Insufficient funds",
                Status = 400,
                Detail = "Amount cannot be withdrawn from the wallet since it has insufficient funds."
            },
            LoyaltyDomainErrorCode.InvalidPointsAmount => new LoyaltyProblemDetails
            {
                Type = "https://docs.loyalty.com/errors/invalid-points-amount",
                Title = "Invalid Points Amount",
                Status = 400,
                Detail = "Points amount is invalid, it cannot be negative."
            },
            LoyaltyDomainErrorCode.CampaignRedeemedMoreThanMaxRedeems => new LoyaltyProblemDetails
            {
                Type = "https://docs.loyalty.com/errors/campaign-redeemed-max-redeems",
                Title = "Campaign redeemed more than max redeems",
                Status = 403,
                Detail = "This campaign cannot be redeemed anymore by this user."
            },
            LoyaltyDomainErrorCode.InvalidDiscountRate => new LoyaltyProblemDetails
            {
                Type = "https://docs.loyalty.com/errors/invalid-discount-rate",
                Title = "Invalid discount rate",
                Status = 400,
                Detail = "Discount rate is invalid, it cannot be non-positive."
            },
            LoyaltyDomainErrorCode.InvalidMaxRedeems => new LoyaltyProblemDetails
            {
                Type = "https://docs.loyalty.com/errors/invalid-max-redeems",
                Title = "Invalid max redeems",
                Status = 400,
                Detail = "Maximum number of redeems per customer for each campaign cannot be non-positive."
            },
            LoyaltyDomainErrorCode.InvalidExpirationDate => new LoyaltyProblemDetails
            {
                Type = "https://docs.loyalty.com/errors/invalid-expiration-date",
                Title = "Invalid expiration date",
                Status = 400,
                Detail = "Expiration date of a campaign cannot be in the past or present."
            },
            LoyaltyDomainErrorCode.InvalidDepositOrWithdrawalAmount => new LoyaltyProblemDetails
            {
                Type = "https://docs.loyalty.com/errors/invalid-deposit-withdrawal-amount",
                Title = "Invalid deposit or withdrawal amount",
                Status = 400,
                Detail = "Amount of points to deposit or withdraw cannot be non-negative."
            },
            _ => new LoyaltyProblemDetails
            {
                Type = "https://docs.loyalty.com/errors/domain-error",
                Title = "Domain error",
                Status = 500,
                Detail = "An unexpected error occured during domain logic."
            }
        };
    }
}