using VenueService.Domain.Entities;

namespace VenueService.Application.DTOs;

public class SeatingStateDto
{
    public List<StateSeat> StateSeats;

    public SeatingStateDto(List<StateSeat> stateSeats)
    {
        StateSeats = stateSeats;
    }
}