namespace VenueService.API.DTOs;

/// <summary>
/// Represents a specific seat position on a seating layout or seating state 
/// </summary>
public class ReserveSeatDto
{
    /// <summary>
    /// Letter specifying row that the seat resides on
    /// </summary>
    public char SeatRow { get; set; }
    
    /// <summary>
    /// Seat number specifying the seat on the specified row
    /// </summary>
    public int SeatNumber { get; set; }
    
    /// <summary>
    /// Versioning/concurrency token of the seat
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// Represents a specific seat position on a seating layout or seating state 
    /// </summary>
    /// <param name="seatRow">Letter specifying row that the seat resides on</param>
    /// <param name="seatNumber">Seat number specifying the seat on the specified row</param>
    /// <param name="version">Versioning/concurrency token of the seat</param>
    public ReserveSeatDto(char seatRow, int seatNumber , string version)
    {
        SeatRow = seatRow;
        SeatNumber = seatNumber;
        Version = version;
    }
}