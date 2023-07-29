using MovieService.Domain.Utils;

namespace MovieService.API.DTOs;

/// <summary>
/// Data transfer object for representing new properties of a Movie entity to be updated.
/// </summary>
public class UpdateMovieDto
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

    public UpdateMovieDto(string name, string director, List<string> actors, Genre genre, string summary, string posterImageUri, double rating, long ratingsCount, List<SmartSign> smartSigns, List<Format> formats)
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
    }
}