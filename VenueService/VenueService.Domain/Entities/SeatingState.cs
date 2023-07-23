using VenueService.Domain.Common;
using VenueService.Domain.Utils;

namespace VenueService.Domain.Entities;

public class SeatingState: EntityBase
{
    public Dictionary<char, List<Seat>> State;
    public bool SeatNumbersDescend;
    public int Capacity;
    
    public SeatingState(SeatingLayout seatingLayout, bool seatNumbersDescend = false)
    {
        State = new Dictionary<char, List<Seat>>();
        SeatNumbersDescend = seatNumbersDescend;
        Capacity = 0;
        
        foreach (var seat in seatingLayout.Layout)
        {
            var row = new List<Seat>();
            
            var seatNumber = seatNumbersDescend ? seat.Value.Count : 1;

            foreach (var seatType in seat.Value)
            {
                row.Add(new Seat(seatType, seatNumber));

                if (seatType == SeatType.Double)
                {
                    if (SeatNumbersDescend) seatNumber -= 2;
                    else seatNumber += 2;
                    Capacity += 2;
                }
                else if (seatType != SeatType.Empty)
                {
                    if (SeatNumbersDescend) --seatNumber;
                    else ++seatNumber;
                    ++Capacity;
                }
            }

            State[seat.Key] = row;
        }
    }

    private Seat GetSeat(char rowLetter, int seatNumber)
    {
        if (rowLetter is < 'A' or > 'Z') throw new Exception();
        var row = State[rowLetter];
        var seat = SeatNumbersDescend ? row[^seatNumber] : row[seatNumber - 1];
        if (seat.SeatNumber != seatNumber) throw new Exception();
        return seat;
    }

    private void SetSeat(Seat seat, char rowLetter, int seatNumber)
    {
        if (rowLetter is < 'A' or > 'Z') throw new Exception();
        var row = State[rowLetter];
        var index = SeatNumbersDescend ? row.Count - seatNumber : seatNumber - 1;
        row[index] = seat;
    }

    public void OccupySeat(char rowLetter, int seatNumber)
    {
        var seat = GetSeat(rowLetter, seatNumber);

        if (seat.Type == SeatType.Double)
        {
            if (Capacity < 2) throw new Exception();
        }
        else
        {
            if (Capacity < 1) throw new Exception();
        }
            
        seat.Occupy();
        SetSeat(seat, rowLetter, seatNumber);
    }
    
    public void ReleaseSeat(char rowLetter, int seatNumber)
    {
        var seat = GetSeat(rowLetter, seatNumber);
        seat.Release();
        SetSeat(seat, rowLetter, seatNumber);

        if (seat.Type == SeatType.Double)
        {
            Capacity += 2;
        }
        else
        {
            ++Capacity;
        }
    }
}