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
    public string ConcurrencySecret { get; set; }
    public Guid SeatingStateId { get; set; }
    
    public StateSeat(char row, SeatType type, int seatNumber)
    {
        //TODO: Workaround for not using Add
        //Id = Guid.NewGuid();

        //SeatingStateId = seatingStateId;
        
        Row = row;
        Occupied = false;
        Type = type;
        SeatNumber = seatNumber;
        ConcurrencySecret = ConcurrencyTokenHelper.GenerateConcurrencySecret();
        Version = 0;
    }

    public void Occupy()
    {
        if (Occupied) throw new VenueDomainException(VenueDomainErrorCode.SeatOccupied);
        if (Type == SeatType.Empty) throw new VenueDomainException(VenueDomainErrorCode.SeatDoesNotExist);

        ++Version;
        Occupied = true;
    }

    public void Release()
    {
        if (!Occupied) throw new VenueDomainException(VenueDomainErrorCode.SeatIsNotOccupied);
        if (Type == SeatType.Empty) throw new VenueDomainException(VenueDomainErrorCode.SeatDoesNotExist);
        
        --Version;
        Occupied = false;
    }

    public StateSeat()
    {
    }
}