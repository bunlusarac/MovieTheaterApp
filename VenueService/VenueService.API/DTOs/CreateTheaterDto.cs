using VenueService.Domain.Entities;
using VenueService.Domain.Utils;

namespace VenueService.API.DTOs;

public class CreateTheaterDto
{
    public string Name { get; set; }
    public TheaterType Type { get; set; }
    public int Width { get; set; }
    
    public CreateTheaterDto(string name, TheaterType type, int width)
    {
        Name = name;
        Type = type;
        Width = width;
    }
}