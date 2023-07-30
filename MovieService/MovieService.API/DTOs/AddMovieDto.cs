using MovieService.Domain.Utils;

namespace MovieService.API.DTOs;

/// <summary>
/// Data transfer object for representing Movie entity to be created.
/// </summary>
public class AddMovieDto
{
    public string Name { get; set; }
    public string Director { get; set; }
    public List<string> Actors { get; set; }
    public Genre Genre { get; set; }
    public string Summary { get; set; }
    public string PosterImageUri { get; set; }
    public List<SmartSign> SmartSigns { get; set; }
    public List<Format> Formats { get; set; }
    public ReleaseStatus ReleaseStatus { get; set; }
    public DateTime ReleaseDate { get; set; }

    public AddMovieDto(string name,
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
    }
}