using MovieService.Domain.Common;
using MovieService.Domain.Exceptions;
using MovieService.Domain.Utils;
using MovieService.Persistence.Exceptions;

namespace MovieService.Domain.Entities;

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
    public bool IsUpcoming { get; set; }
    public bool IsFeatured { get; set; }

    public Movie(string name,
        string director,
        List<string> actors,
        Genre genre,
        string summary,
        string posterImageUri,
        double rating,
        long ratingsCount,
        List<SmartSign> smartSigns,
        List<Format> formats,
        bool isUpcoming,
        bool isFeatured)
    {
        Name = name;
        Director = director;
        Actors = actors;
        Genre = genre;
        Summary = summary;
        PosterImageUri = posterImageUri;
        Rating = rating;
        RatingsCount = ratingsCount;
        SmartSigns = smartSigns;
        Formats = formats;
        IsUpcoming = isUpcoming;
        IsFeatured = isFeatured;
    }

    public void UpdateRating(int newRating)
    {
        if (newRating is < 1 or > 10) throw new MovieDomainException(MovieDomainErrorCode.InvalidRatingValue);
        Rating = (Rating * RatingsCount + newRating) / (RatingsCount + 1);
    }

    public void SetAsUpcoming()
    {
        if (IsUpcoming) throw new MovieDomainException(MovieDomainErrorCode.MovieAlreadyUpcoming);
        if (IsFeatured) IsFeatured = false;
        IsUpcoming = true;
    }
    
    public void SetAsFeatured()
    {
        if (IsFeatured) throw new MovieDomainException(MovieDomainErrorCode.MovieAlreadyFeatured);
        if (IsUpcoming) IsUpcoming = false; 
        IsFeatured = true;
    }

    public void UnsetAsFeatured()
    {
        if (IsFeatured == false) throw new MovieDomainException(MovieDomainErrorCode.MovieAlreadyNotFeatured);
        IsFeatured = false;
    }
}