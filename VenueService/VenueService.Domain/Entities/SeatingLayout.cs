using VenueService.Domain.Common;
using VenueService.Domain.Utils;

namespace VenueService.Domain.Entities;

public class SeatingLayout: EntityBase
{
    public List<LayoutSeat> LayoutSeats;
    public int Width;
    public char LastRow;

    public SeatingLayout(int width)
    {
        LastRow = 'A';
        Width = width;
        LayoutSeats = new List<LayoutSeat>();
    }

    public void AddRow(List<SeatType> seats)
    {
        var size = 0;
        var layoutSeats = new List<LayoutSeat>();

        foreach (var seat in seats)
        {
            if (seat == SeatType.Double) size += 2;
            else ++size;

            if (size > Width) throw new Exception();
            layoutSeats.Add(new LayoutSeat(LastRow, size, seat));
        }

        LayoutSeats.AddRange(layoutSeats);
        if (LastRow < 'Z') ++LastRow;
    }

    public void AddRows(List<SeatType> seats, int times)
    {
        for (int i = 0; i < times; i++)
        {
            AddRow(seats);
        }
    }

    public SeatingLayout()
    {
    }
}