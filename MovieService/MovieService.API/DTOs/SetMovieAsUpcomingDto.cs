namespace MovieService.API.DTOs;

public class SetMovieAsUpcomingDto
{
    public DateTime ReleaseDate { get; set; }

    public SetMovieAsUpcomingDto(DateTime releaseDate)
    {
        ReleaseDate = releaseDate;
    }
}