using VenueService.Domain.Common;
using VenueService.Domain.Utils;

namespace VenueService.Domain.Entities;

public class LayoutSeat: EntityBase
{
    public char Row { get; set; }
    public int SeatNumber { get; set; }
    public SeatType SeatType { get; set; }
    public Guid LayoutId { get; set; }
    
    public LayoutSeat(char row, int seatIndex, SeatType seatType)
    {
        Row = row;
        SeatNumber = seatIndex;
        SeatType = seatType;
    }

    public LayoutSeat()
    {
    }
}