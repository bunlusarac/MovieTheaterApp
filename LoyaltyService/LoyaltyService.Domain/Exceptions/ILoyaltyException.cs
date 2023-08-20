namespace LoyaltyService.Domain.Exceptions;

public interface ILoyaltyException
{
    public LoyaltyProblemDetails ProblemDetails { get; set; }
}