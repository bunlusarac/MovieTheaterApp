namespace MovieService.Persistence.Exceptions;

public enum MovieDomainErrorCode
{
    InvalidRatingValue,
    MovieAlreadyUpcoming,
    MovieAlreadyFeatured,
    MovieAlreadyNotFeatured,
}