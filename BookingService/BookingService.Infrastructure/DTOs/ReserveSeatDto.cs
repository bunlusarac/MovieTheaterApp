namespace BookingService.Infrastructure.DTOs;

public class ReserveSeatDto
{
    public char SeatRow { get; set; }
    public int SeatNumber { get; set; }
    public string Version { get; set; }
}