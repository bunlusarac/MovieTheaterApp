using MovieService.Persistence.Exceptions;

namespace MovieService.Domain.Exceptions;

public class MovieDomainException: Exception
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public string Detail { get; set; }
    public string Instance { get; set; }

    public MovieDomainException(MovieDomainErrorCode errorCode)
    {
        switch (errorCode)
        {
            case MovieDomainErrorCode.InvalidRatingValue:
                Type = "https://docs.movie.com/errors/invalid-rating-value";
                Title = "Invalid rating value";
                Status = 400;
                Detail = "Given rating value is invalid: it must be an integer inclusively between 1 and 10.";
                
                break;
            default:
                break;
        }
    }
}