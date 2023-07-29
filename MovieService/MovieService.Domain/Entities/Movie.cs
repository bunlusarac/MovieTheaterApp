using MovieService.Domain.Common;
using MovieService.Domain.Exceptions;
using MovieService.Domain.Utils;
using MovieService.Persistence.Exceptions;

namespace MovieService.Domain.Entities;

/// <summary>
/// Entity that represents movies and contains locators and further information about them. 
/// </summary>
public class Movie: EntityBase
{
    public string Name { get; set; }
    public string Director { get; set; }
    public List<string> Actors { get; set; }
    public Genre Genre { get; set; }
    public string Summary { get; set; }
    public string PosterImageUri { get; set; }
    public double Rating { get; set; }
    public long RatingsCount { get; set; }
    public List<SmartSign> SmartSigns { get; set; }
    public List<Format> Formats { get; set; }
    public ReleaseStatus ReleaseStatus { get; set; }
    public DateTime ReleaseDate { get; set; }

    public Movie(string name,
        string director,
        List<string> actors,
        Genre genre,
        string summary,
        string posterImageUri,
        List<SmartSign> smartSigns,
        List<Format> formats,
        ReleaseStatus releaseStatus,
        DateTime releaseDate)
    {
        // If it is upcoming, a release date later than now must be provided.
        if(releaseStatus == ReleaseStatus.Upcoming && releaseDate <= DateTime.UtcNow) 
            throw new MovieDomainException(MovieDomainErrorCode.InvalidReleaseDate);
        
        Name = name;
        Director = director;
        Actors = actors;
        Genre = genre;
        Summary = summary;
        PosterImageUri = posterImageUri;
        SmartSigns = smartSigns;
        Formats = formats;
        ReleaseStatus = releaseStatus;
        ReleaseDate = releaseDate;

        Rating = 0;
        RatingsCount = 0;
    }

    /// <summary>
    /// Add a rating value to movie's current rating and update the mean rating.
    /// </summary>
    /// <param name="newRating">New rating value</param>
    /// <exception cref="MovieDomainException">Thrown when <c>newRating</c> is invalid.</exception>
    public void UpdateRating(int newRating)
    {
        if (newRating is < 1 or > 10) throw new MovieDomainException(MovieDomainErrorCode.InvalidRatingValue);
        Rating = (Rating * RatingsCount + newRating) / (RatingsCount + 1);
        ++RatingsCount;
    }

    /// <summary>
    /// Set the movie as upcoming, i.e. as an unreleased movie with determined release date in the future.
    /// At <c>releaseDate</c>, this movie is planned to be in the release status <c>ReleaseStatus.InTheaters</c>.  
    /// </summary>
    /// <param name="releaseDate">Determined release date of the movie</param>
    /// <exception cref="MovieDomainException">Thrown when movie is already released, i.e. the movie is already
    /// on release status <c>ReleaseStatus.Upcoming</c></exception>
    public void SetAsUpcoming(DateTime releaseDate)
    {
        if (ReleaseStatus == ReleaseStatus.Upcoming) 
            throw new MovieDomainException(MovieDomainErrorCode.MovieAlreadyUpcoming);

        ReleaseDate = releaseDate;
        ReleaseStatus = ReleaseStatus.Upcoming;
    }

    /// <summary>
    /// Set the movie as in theaters, i.e. as a recently released movie.   
    /// </summary>
    /// <exception cref="MovieDomainException">Thrown when movie is already in theaters, i.e. the movie is already
    /// on release status <c>ReleaseStatus.MovieAlreadyFeatured</c>.</exception>
    public void SetAsInTheaters()
    {
        if (ReleaseStatus == ReleaseStatus.InTheaters) 
            throw new MovieDomainException(MovieDomainErrorCode.MovieAlreadyInTheaters);

        ReleaseStatus = ReleaseStatus.InTheaters;
    }

    /// <summary>
    /// Set the movie as released, i.e. as a movie that has been released a long time ago.
    /// </summary>
    /// <exception cref="MovieDomainException">Thrown when movie is already released, i.e. the movie is already
    /// on release status <c>ReleaseStatus.Released</c>.</exception>
    public void SetAsReleased()
    {
        if (ReleaseStatus == ReleaseStatus.Released)
            throw new MovieDomainException(MovieDomainErrorCode.MovieAlreadyReleased);
    }
}