using VenueService.Domain.Common;
using VenueService.Domain.Exceptions;
using VenueService.Domain.Utils;

namespace VenueService.Domain.Entities;

public class SeatingState: EntityBase
{
    public virtual List<StateSeat> StateSeats { get; set; }
    public int Capacity { get; set; }
    public Guid SessionId { get; set; }
    public char LastRow { get; set; } = 'A';
    
    public SeatingState(SeatingLayout seatingLayout, bool seatNumbersDescend = false)
    {
        //TODO: Workaround for not using Add
        //Id = Guid.NewGuid();
        
        StateSeats = new List<StateSeat>();
        Capacity = 0;
        LastRow = seatingLayout.LastRow;
        
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

    public StateSeat GetSeat(char rowLetter, int seatNumber)
    {
        if (rowLetter < 'A' || rowLetter > LastRow) throw new VenueDomainException(VenueDomainErrorCode.SeatRowOutOfBounds);
        var seat = StateSeats.First(s => s.SeatNumber == seatNumber && s.Row == rowLetter);
        return seat;
    }

    public void SetSeat(StateSeat stateSeat)
    {
        var idx = StateSeats.FindIndex(s => stateSeat.Id == s.Id);
        if (idx == -1) throw new VenueDomainException(VenueDomainErrorCode.SeatDoesNotExist);
        StateSeats[idx] = stateSeat;
    }

    public void OccupySeat(char rowLetter, int seatNumber, string concurrencyToken)
    {
        var seat = GetSeat(rowLetter, seatNumber);

        if (!ConcurrencyTokenHelper.ValidateConcurrencyToken(seat.Version, seat.ConcurrencySecret, concurrencyToken)) 
            throw new VenueDomainException(VenueDomainErrorCode.SeatVersionExpired);
        
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
    
    public void ReleaseSeat(char rowLetter, int seatNumber/*, string concurrencyToken*/)
    {
        var seat = GetSeat(rowLetter, seatNumber);

        /*
        if (!ConcurrencyTokenHelper.ValidateConcurrencyToken(seat.Version, seat.ConcurrencySecret, concurrencyToken)) 
            throw new VenueDomainException(VenueDomainErrorCode.SeatVersionExpired);
        */
        
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