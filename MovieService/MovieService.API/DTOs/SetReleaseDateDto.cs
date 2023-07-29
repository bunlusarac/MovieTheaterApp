namespace MovieService.API.DTOs;

public class SetReleaseDateDto
{
    public DateTime ReleaseDate { get; set; }

    public SetReleaseDateDto(DateTime releaseDate)
    {
        ReleaseDate = releaseDate;
    }
}