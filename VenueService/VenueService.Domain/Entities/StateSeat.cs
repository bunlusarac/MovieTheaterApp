using VenueService.Domain.Common;
using VenueService.Domain.Utils;

namespace VenueService.Domain.Entities;

public class StateSeat: EntityBase
{
    public char Row { get; set; }
    public bool Occupied { get; set; }
    public SeatType Type { get; set; }
    public int SeatNumber { get; set; }
    
    public StateSeat(SeatType type, int seatNumber)
    {
        Type = type;
        SeatNumber = seatNumber;
        Occupied = false;
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

    public StateSeat()
    {
    }
}