using System.ComponentModel.DataAnnotations;
using VenueService.Domain.Common;
using VenueService.Domain.Exceptions;
using VenueService.Domain.Utils;

namespace VenueService.Domain.Entities;

public class StateSeat: EntityBase
{
    public char Row { get; set; }
    public bool Occupied { get; set; }
    public SeatType Type { get; set; }
    public int SeatNumber { get; set; }
    
    public uint Version { get; set; }
    
    public StateSeat(char row, SeatType type, int seatNumber)
    {
        Row = row;
        Occupied = false;
        Type = type;
        SeatNumber = seatNumber;
    }

    public void Occupy()
    {
        if (Occupied) throw new VenueDomainException(VenueDomainErrorCode.SeatOccupied);
        if (Type == SeatType.Empty) throw new VenueDomainException(VenueDomainErrorCode.SeatDoesNotExist);
        Occupied = true;
    }

    public void Release()
    {
        if (!Occupied) throw new VenueDomainException(VenueDomainErrorCode.SeatIsNotOccupied);
        if (Type == SeatType.Empty) throw new VenueDomainException(VenueDomainErrorCode.SeatDoesNotExist);
        Occupied = false;
    }

    public StateSeat()
    {
    }
}