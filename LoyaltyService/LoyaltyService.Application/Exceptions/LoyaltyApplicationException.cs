using LoyaltyService.Application.Exceptions;
using LoyaltyService.Domain.Exceptions;

namespace LoyaltyService.Application.Exceptions;

public class LoyaltyApplicationException : Exception
{
    public LoyaltyProblemDetails ProblemDetails { get; set; }
    
    public LoyaltyApplicationException(LoyaltyApplicationErrorCode applicationErrorCode)
    {
        ProblemDetails = GetProblemDetailsFromErrorCode(applicationErrorCode);
    }
    
    public LoyaltyApplicationException(LoyaltyApplicationErrorCode applicationErrorCode, Exception innerException) :
        base(GetProblemDetailsFromErrorCode(applicationErrorCode).Detail, innerException)
    {
        ProblemDetails = GetProblemDetailsFromErrorCode(applicationErrorCode);
    }
    
    private static LoyaltyProblemDetails GetProblemDetailsFromErrorCode(LoyaltyApplicationErrorCode applicationErrorCode)
    {
        return applicationErrorCode switch
        {
            LoyaltyApplicationErrorCode.VersionExpired => new LoyaltyProblemDetails
            {
                Type = "https://docs.loyalty.com/errors/version-expired", 
                Title = "Version expired",
                Status = 404,
                Detail = "Campaign you are trying to redeem have been changed by the time you have queried it."
            },
            LoyaltyApplicationErrorCode.UserAlreadyExists => new LoyaltyProblemDetails
            {
                Type = "https://docs.loyalty.com/errors/user-already-exists", 
                Title = "User already exists",
                Status = 409,
                Detail = "There already exists a loyalty customer for this customer ID."
            },
            _ => new LoyaltyProblemDetails
            {
                Type = "https://docs.loyalty.com/errors/persistence-error",
                Title = "Persistence error",
                Status = 500,
                Detail = "An unexpected error occured during application logic."
            }
        };
    }
}