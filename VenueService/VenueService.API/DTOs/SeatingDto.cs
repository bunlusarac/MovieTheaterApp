namespace VenueService.API.DTOs;

public class SeatingDto
{
    public int SeatRow { get; set; }
    public int SeatNumber { get; set; }

    public SeatingDto(int seatRow, int seatNumber)
    {
        SeatRow = seatRow;
        SeatNumber = seatNumber;
    }
}