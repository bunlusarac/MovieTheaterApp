using VenueService.Domain.Common;
using VenueService.Domain.Exceptions;
using VenueService.Domain.Utils;


namespace VenueService.Domain.Entities;

public class SeatingLayout: EntityBase
{
    public virtual List<LayoutSeat> LayoutSeats { get; set; }
    public int Width { get; set; }
    public char LastRow { get; set; }
    public Guid TheaterId { get; set; }

    public SeatingLayout(int width)
    {
        if (width < 0) throw new VenueDomainException(VenueDomainErrorCode.NegativeLayoutWidth);
        
        LastRow = 'A';
        Width = width;
        LayoutSeats = new List<LayoutSeat>();
    }

    public void AddRow(List<SeatType> seats)
    {
        if (LastRow == 'Z' + 1) throw new VenueDomainException(VenueDomainErrorCode.MaximumLayoutHeightOverflow);
        
        var size = 0;
        var layoutSeats = new List<LayoutSeat>();

        foreach (var seat in seats)
        {
            if (seat == SeatType.Double) size += 2;
            else ++size;

            if (size > Width) throw new VenueDomainException(VenueDomainErrorCode.LayoutWidthOverflow);
            layoutSeats.Add(new LayoutSeat(LastRow, size, seat));
        }

        LayoutSeats.AddRange(layoutSeats);
        ++LastRow;
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