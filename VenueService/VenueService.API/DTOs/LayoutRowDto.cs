using VenueService.Domain.Utils;

namespace VenueService.API.DTOs;

/// <summary>
/// Represents required parameters for creating a new row
/// layout in the theater layout
/// </summary>
public class LayoutRowDto
{
    /// <summary>
    /// List of seat types for the row layout to be added into the theater layout
    /// </summary>
    public List<SeatType> RowSeats { get; set; }
    
    /// <summary>
    /// Number of times the specified row layout will be added into the theater layout
    /// </summary>
    public int Times { get; set; }

    /// <summary>
    /// Represents required parameters for creating a new row
    /// layout in the theater layout
    /// </summary>
    /// <param name="rowSeats">List of seat types for the row layout to be added into the theater layout</param>
    /// <param name="times">Number of times the specified row layout will be added into the theater layout</param>
    public LayoutRowDto(List<SeatType> rowSeats, int times)
    {
        RowSeats = rowSeats;
        Times = times;
    }
}