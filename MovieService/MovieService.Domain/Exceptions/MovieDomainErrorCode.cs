namespace MovieService.Persistence.Exceptions;

/// <summary>
/// An enum for specifying error codes addressing the occured problem in domain logic. 
/// </summary>
public enum MovieDomainErrorCode
{
    InvalidRatingValue,
    InvalidReleaseDate,
    MovieAlreadyUpcoming,
    MovieAlreadyInTheaters,
    MovieAlreadyReleased,
    MovieNotFound
}