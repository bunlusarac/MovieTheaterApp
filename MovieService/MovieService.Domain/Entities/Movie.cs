using MovieService.Domain.Common;
using MovieService.Domain.Utils;

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
    public List<SmartSign> SmartSigns { get; set; }
    public List<Format> Formats { get; set; }

    public Movie(string name, string director, List<string> actors, Genre genre, string summary, string posterImageUri, double rating, List<SmartSign> smartSigns, List<Format> formats)
    {
        Name = name;
        Director = director;
        Actors = actors;
        Genre = genre;
        Summary = summary;
        PosterImageUri = posterImageUri;
        Rating = rating;
        SmartSigns = smartSigns;
        Formats = formats;
    }
}