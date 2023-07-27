using VenueService.Domain.Common;
using VenueService.Domain.Exceptions;
using VenueService.Domain.Utils;

namespace VenueService.Domain.Entities;

public class SeatingState: EntityBase
{
    public virtual List<StateSeat> StateSeats { get; set; }
    public int Capacity { get; set; }
    public Guid SessionId { get; set; }
    
    public SeatingState(SeatingLayout seatingLayout, bool seatNumbersDescend = false)
    {
        StateSeats = new List<StateSeat>();
        Capacity = 0;
        
        foreach (var seat in seatingLayout.LayoutSeats)
        {
            StateSeats.Add(new StateSeat(seat.Row, seat.SeatType, seat.SeatNumber));
            
            if (seat.SeatType == SeatType.Double)
            {
                Capacity += 2;
            }
            else if (seat.SeatType != SeatType.Empty)
            {
                ++Capacity;
            }
        }
    }

    private StateSeat GetSeat(char rowLetter, int seatNumber)
    {
        if (rowLetter is < 'A' or > 'Z') throw new VenueDomainException(VenueDomainErrorCode.SeatRowOutOfBounds);
        var seat = StateSeats.First(s => s.SeatNumber == seatNumber && s.Row == rowLetter);
        return seat;
    }

    private void SetSeat(StateSeat stateSeat)
    {
        var idx = StateSeats.FindIndex(s => stateSeat.Id == s.Id);
        if (idx == -1) throw new VenueDomainException(VenueDomainErrorCode.SeatDoesNotExist);
        StateSeats[idx] = stateSeat;
    }

    public void OccupySeat(char rowLetter, int seatNumber)
    {
        var seat = GetSeat(rowLetter, seatNumber);

        if (seat.Type == SeatType.Double)
        {
            if (Capacity < 2) throw new VenueDomainException(VenueDomainErrorCode.TheaterCapacityIsFull);
            Capacity -= 2;
        }
        else
        {
            if (Capacity < 1) throw new VenueDomainException(VenueDomainErrorCode.TheaterCapacityIsFull);
            --Capacity;
        }

        seat.Occupy();
        SetSeat(seat);
    }
    
    public void ReleaseSeat(char rowLetter, int seatNumber)
    {
        var seat = GetSeat(rowLetter, seatNumber);
        seat.Release();
        SetSeat(seat);

        if (seat.Type == SeatType.Double)
        {
            Capacity += 2;
        }
        else
        {
            ++Capacity;
        }
    }

    public SeatingState()
    {
    }
}