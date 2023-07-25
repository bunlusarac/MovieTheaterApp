namespace VenueService.API.DTOs;

public class SeatingDto
{
    public char SeatRow { get; set; }
    public int SeatNumber { get; set; }

    public SeatingDto(char seatRow, int seatNumber)
    {
        SeatRow = seatRow;
        SeatNumber = seatNumber;
    }
}