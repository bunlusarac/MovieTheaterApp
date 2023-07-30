using Microsoft.AspNetCore.Mvc;
using MovieService.Persistence.Exceptions;

namespace MovieService.Domain.Exceptions;

/// <summary>
/// This exception is used for addressing any errors occured in Movie entity behavior.
/// </summary>
public class MovieDomainException: Exception
{
    public ProblemDetails ProblemDetails { get; set; }
    
    /// <summary>
    /// For given <c>MovieErrorCode</c> value, this constructor will map ProblemDetails (see: RFC 7807)
    /// parameters for that particular situation. When thrown, a <c>ProblemDetails</c> object will be
    /// filled with these parameters and request will be responded with JSON representation of it. 
    /// </summary>
    /// <param name="domainErrorCode">Error code addressing the situation</param>
    public MovieDomainException(MovieDomainErrorCode domainErrorCode)
    {
        ProblemDetails = GetProblemDetailsFromErrorCode(domainErrorCode);
    }

    /// <summary>
    /// Variant of main constructor with inner exception. This will keep the stack trace when
    /// an MovieDomainException is rethrown from another caught Exception in a try-catch block.
    /// </summary>
    /// <param name="domainErrorCode">Error code addressing the situation</param>
    /// <param name="innerException">Exception that will be wrapped in this exception</param>
    public MovieDomainException(MovieDomainErrorCode domainErrorCode, Exception innerException) :
        base(GetProblemDetailsFromErrorCode(domainErrorCode).Detail, innerException)
    {
        ProblemDetails = GetProblemDetailsFromErrorCode(domainErrorCode);
    }
    
    private static ProblemDetails GetProblemDetailsFromErrorCode(MovieDomainErrorCode domainErrorCode)
    {
        return domainErrorCode switch
        {
            MovieDomainErrorCode.InvalidRatingValue => new ProblemDetails
            {
                Type = "https://docs.movie.com/errors/invalid-rating-value", 
                Title = "Invalid rating value",
                Status = 400,
                Detail = "Given rating value is invalid: it must be an integer inclusively between 1 and 10."
            },
            MovieDomainErrorCode.MovieAlreadyInTheaters => new ProblemDetails
            {
                Type = "https://docs.movie.com/errors/movie-already-intheaters",
                Title = "Movie is already In Theaters",
                Status = 400,
                Detail = "The movie you are trying to set as In Theaters is already in release state In Theaters."
            },
            MovieDomainErrorCode.MovieAlreadyUpcoming => new ProblemDetails
            {
                Type = "https://docs.movie.com/errors/movie-already-upcoming",
                Title = "Movie is already Upcoming",
                Status = 400,
                Detail = "The movie you are trying to set as Upcoming is already in release state Upcoming."
            },
            MovieDomainErrorCode.MovieAlreadyReleased => new ProblemDetails
            {
                Type = "https://docs.movie.com/errors/movie-already-released",
                Title = "Movie is already Released",
                Status = 400,
                Detail = "The movie you are trying to set as Released is already in release state Released."
            },
            MovieDomainErrorCode.InvalidReleaseDate => new ProblemDetails
            {
                Type = "https://docs.movie.com/errors/invalid-release-date",
                Title = "Invalid release date",
                Status = 400,
                Detail = "The movie entity is initialized with release state Upcoming but the provided release date is in the past."
            },
            _ => new ProblemDetails
            {
                Type = "https://docs.movie.com/errors/domain-error",
                Title = "Domain error",
                Status = 500,
                Detail = "An unexpected error occured during domain logic."
            }
        };
    }
}