using VenueService.Domain.Entities;
using VenueService.Domain.Utils;

namespace VenueService.API.DTOs;

/// <summary>
/// Represents required parameters for theater creation
/// </summary>
public class CreateTheaterDto
{
    /// <summary>
    /// Name of the theater
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Type of the theater (Standard 2D, IMAX...)
    /// </summary>
    public TheaterType Type { get; set; }
    
    /// <summary>
    /// Width of the theater as in number of single seats
    /// </summary>
    public int Width { get; set; }
    
    /// <summary>
    /// Represents required parameters for theater creation
    /// </summary>
    /// <param name="name">Name of the theater</param>
    /// <param name="type">Type of the theater (Standard 2D, IMAX...)</param>
    /// <param name="width">Width of the theater as in number of single seats</param>
    public CreateTheaterDto(string name, TheaterType type, int width)
    {
        Name = name;
        Type = type;
        Width = width;
    }
}