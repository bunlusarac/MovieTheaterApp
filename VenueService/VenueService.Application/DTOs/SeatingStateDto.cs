using VenueService.Domain.Entities;

namespace VenueService.Application.DTOs;

public class SeatingStateDto
{
    public Dictionary<char, List<Seat>> State;

    public SeatingStateDto()
    {
        State = new Dictionary<char, List<Seat>>();
    }
}