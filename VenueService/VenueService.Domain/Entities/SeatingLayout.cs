using VenueService.Domain.Common;
using VenueService.Domain.Utils;

namespace VenueService.Domain.Entities;

public class SeatingLayout: EntityBase
{
    public Dictionary<char, List<SeatType>> Layout;
    public int Width;
    public char LastRow;

    public SeatingLayout(int width)
    {
        LastRow = 'A';
        Width = width;
        Layout = new Dictionary<char, List<SeatType>>();
    }

    public void AddRow(List<SeatType> seats)
    {
        var size = 0;
        
        foreach (var seat in seats)
        {
            if (seat == SeatType.Double) size += 2;
            else ++size;

            if (size > Width) throw new Exception();
        }

        Layout[LastRow] = seats;
        ++LastRow;

        if (LastRow == 'Z') throw new Exception();
    }
}