using Microsoft.AspNetCore.Mvc;

namespace MovieService.Persistence.Exceptions;

/// <summary>
/// This exception is used for addressing any errors occured in persistence of entities.
/// </summary>
public class MoviePersistenceException : Exception
{
    public ProblemDetails ProblemDetails { get; set; }

    /// <summary>
    /// For given <c>MovieErrorCode</c> value, this constructor will map ProblemDetails (see: RFC 7807)
    /// parameters for that particular situation. When thrown, a <c>ProblemDetails</c> object will be
    /// filled with these parameters and request will be responded with JSON representation of it. 
    /// </summary>
    /// <param name="persistenceErrorCode">Error code addressing the situation</param>
    public MoviePersistenceException(MoviePersistenceErrorCode persistenceErrorCode)
    {
        ProblemDetails = GetProblemDetailsFromErrorCode(persistenceErrorCode);
    }
    
    /// <summary>
    /// Variant of main constructor with inner exception. This will keep the stack trace when
    /// an MoviePersistenceException is rethrown from another caught Exception in a try-catch block.
    /// </summary>
    /// <param name="persistenceErrorCode">Error code addressing the situation</param>
    /// <param name="innerException">Exception that will be wrapped in this exception</param>
    public MoviePersistenceException(MoviePersistenceErrorCode persistenceErrorCode, Exception innerException) : 
        base(GetProblemDetailsFromErrorCode(persistenceErrorCode).Detail, innerException)
    {
        ProblemDetails = GetProblemDetailsFromErrorCode(persistenceErrorCode);
    }

    private static ProblemDetails GetProblemDetailsFromErrorCode(MoviePersistenceErrorCode persistenceErrorCode)
    {
        return persistenceErrorCode switch
        {
            MoviePersistenceErrorCode.MovieDeletionFailed => new ProblemDetails
            {
                Type = "https://docs.movie.com/errors/movie-deletion-failed",
                Title = "Movie deletion failed",
                Status = 500,
                Detail = "A persistence error occured during deletion.",
            },
            MoviePersistenceErrorCode.MovieNotFound => new ProblemDetails
            {
                Type = "https://docs.movie.com/errors/movie-not-found",
                Title = "Movie not found",
                Status = 404,
                Detail = "Movie with given ID could not be found.",
            },
            MoviePersistenceErrorCode.MovieUpdateFailed => new ProblemDetails
            {
                Type = "https://docs.movie.com/errors/movie-update-failed",
                Title = "Movie update failed",
                Status = 500,
                Detail = "A persistence error occured during update.",
            },
            MoviePersistenceErrorCode.MovieAdditionFailed => new ProblemDetails
            {
                Type = "https://docs.movie.com/errors/movie-addition-failed",
                Title = "Movie addition failed",
                Status = 500,
                Detail = "A persistence error occured during addition.",
            },
            _ => new ProblemDetails
            {
                Type = "https://docs.movie.com/errors/persistence-error",
                Title = "Persistence error",
                Status = 500,
                Detail = "An unexpected error occured during persistence.",
            }
        };
    }
    
    
}