using BookingService.Domain.Utils;

namespace BookingService.Application.DTOs;

public class MovieDto
{
    public string Name { get; set; }
    public string Director { get; set; }
    public List<string> Actors { get; set; }
    public string Genre { get; set; }
    public string Summary { get; set; }
    public string PosterImageUri { get; set; }
    public double Rating { get; set; }
    public long RatingsCount { get; set; }
    public List<SmartSign> SmartSigns { get; set; }
    public List<string> Formats { get; set; }
    public string ReleaseStatus { get; set; }
    public DateTime ReleaseDate { get; set; }
}