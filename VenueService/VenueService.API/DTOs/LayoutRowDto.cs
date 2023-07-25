using VenueService.Domain.Utils;

namespace VenueService.API.DTOs;

public class LayoutRowDto
{
    public List<SeatType> RowSeats { get; set; }
    public int Times { get; set; }

    public LayoutRowDto(List<SeatType> rowSeats, int times)
    {
        RowSeats = rowSeats;
        Times = times;
    }
}