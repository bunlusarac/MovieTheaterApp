using LoyaltyService.Domain.Exceptions;

namespace LoyaltyService.Persistence.Exceptions;

public class LoyaltyPersistenceException: Exception
{
    public LoyaltyProblemDetails ProblemDetails { get; set; }
    
    public LoyaltyPersistenceException(LoyaltyPersistenceErrorCode persistenceErrorCode)
    {
        ProblemDetails = GetProblemDetailsFromErrorCode(persistenceErrorCode);
    }
    
    public LoyaltyPersistenceException(LoyaltyPersistenceErrorCode persistenceErrorCode, Exception innerException) :
        base(GetProblemDetailsFromErrorCode(persistenceErrorCode).Detail, innerException)
    {
        ProblemDetails = GetProblemDetailsFromErrorCode(persistenceErrorCode);
    }
    
    private static LoyaltyProblemDetails GetProblemDetailsFromErrorCode(LoyaltyPersistenceErrorCode persistneceErrorCode)
    {
        return persistneceErrorCode switch
        {
            LoyaltyPersistenceErrorCode.NotFound => new LoyaltyProblemDetails
            {
                Type = "https://docs.loyalty.com/errors/not-found", 
                Title = "Not found",
                Status = 404,
                Detail = "The entity with given ID could not be found."
            },
            LoyaltyPersistenceErrorCode.UpdateFailed => new LoyaltyProblemDetails
            {
                Type = "https://docs.loyalty.com/errors/update-failed", 
                Title = "Update failed",
                Status = 500,
                Detail = "The entity could not be updated due to an error."
            },
            LoyaltyPersistenceErrorCode.AddFailed => new LoyaltyProblemDetails
            {
                Type = "https://docs.loyalty.com/errors/add-failed", 
                Title = "Addition failed",
                Status = 500,
                Detail = "The entity could not be added due to an error."
            },
            LoyaltyPersistenceErrorCode.DeleteFailed => new LoyaltyProblemDetails
            {
                Type = "https://docs.loyalty.com/errors/delete-failed", 
                Title = "Deletion failed",
                Status = 500,
                Detail = "The entity could not be deleted due to an error."
            },
            _ => new LoyaltyProblemDetails
            {
                Type = "https://docs.loyalty.com/errors/persistence-error",
                Title = "Persistence error",
                Status = 500,
                Detail = "An unexpected error occured during persistence."
            }
        };
    }
}