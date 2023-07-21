using VenueService.Domain.Common;
using VenueService.Domain.Utils;

namespace VenueService.Domain.Entities;

public class Seat: EntityBase
{
    public SeatType Type;
    public bool Occupied;
    public int SeatNumber;
    
    public Seat(SeatType type, int seatNumber)
    {
        Type = type;
        SeatNumber = seatNumber;
    }

    public void Occupy()
    {
        if (Occupied && Type == SeatType.Empty) throw new Exception();
        Occupied = true;
    }

    public void Release()
    {
        if (!Occupied && Type == SeatType.Empty) throw new Exception();
        Occupied = false;
    }
}