using VenueService.Domain.Entities;
using VenueService.Domain.Utils;

namespace VenueService.Application.DTOs;

/// <summary>
/// Represents state of an individual seat of a session 
/// </summary>
public class SeatingStateWithVersioningDto
{
    /// <summary>
    /// Letter specifying row that the seat resides on
    /// </summary>
    public char Row { get; set; }
    
    /// <summary>
    /// Flag indicating whether the seat is occupied or not
    /// </summary>
    public bool Occupied { get; set; }
    
    /// <summary>
    /// Type of the seat, e.g. Single, Double...
    /// </summary>
    public SeatType Type { get; set; }
    
    /// <summary>
    /// Seat number specifying the seat on the specified row
    /// </summary>
    public int SeatNumber { get; set; }

    /// <summary>
    /// Versioning information for concurrency moderation
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// Represents state of an individual seat of a session 
    /// </summary>
    /// <param name="row">Letter specifying row that the seat resides on</param>
    /// <param name="occupied">Flag indicating whether the seat is occupied or not</param>
    /// <param name="type">Type of the seat, e.g. Single, Double...</param>
    /// <param name="seatNumber">Seat number specifying the seat on the specified row</param>
    /// <param name="version">Concurrency token/version of the seat</param>

    public SeatingStateWithVersioningDto(char row, bool occupied, SeatType type, int seatNumber, string version)
    {
        Row = row;
        Occupied = occupied;
        Type = type;
        SeatNumber = seatNumber;
        Version = version;
    }
}