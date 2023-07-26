namespace VenueService.API.DTOs;

/// <summary>
/// Represents a specific seat position on a seating layout or seating state 
/// </summary>
public class SeatingDto
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
    /// Represents a specific seat position on a seating layout or seating state 
    /// </summary>
    /// <param name="seatRow">Letter specifying row that the seat resides on</param>
    /// <param name="seatNumber">Seat number specifying the seat on the specified row</param>
    public SeatingDto(char seatRow, int seatNumber)
    {
        SeatRow = seatRow;
        SeatNumber = seatNumber;
    }
}