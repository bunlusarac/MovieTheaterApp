using MediatR;
using VenueService.Application.DTOs;
using VenueService.Application.Exceptions;
using VenueService.Application.Persistence;
using VenueService.Domain.Entities;

namespace VenueService.Application.Queries;

public class GetSessionSeatingStateQuery: IRequest<List<SeatingStateDto>>
{
    public Guid VenueId { get; set; }
    public Guid TheaterId { get; set; }
    public Guid SessionId { get; set; }

    public GetSessionSeatingStateQuery(Guid venueId, Guid theaterId, Guid sessionId)
    {
        VenueId = venueId;
        TheaterId = theaterId;
        SessionId = sessionId;
    }
}

public class GetSessionSeatingStateQueryHandler : IRequestHandler<GetSessionSeatingStateQuery, List<SeatingStateDto>>
{
    private readonly IVenueRepository _venueRepository;

    public GetSessionSeatingStateQueryHandler(IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository;
    }
    
    public async Task<List<SeatingStateDto>> Handle(GetSessionSeatingStateQuery request, CancellationToken cancellationToken)
    {
        var venue = await _venueRepository.GetById(request.VenueId);
        if (venue == null) throw new VenueApplicationException(VenueApplicationErrorCode.VenueDoesNotExist);

        var theater = venue.Theaters.FirstOrDefault(s => s.Id == request.TheaterId);
        if (theater == null) throw new VenueApplicationException(VenueApplicationErrorCode.TheaterDoesNotExist);
        
        var session = theater.Sessions.FirstOrDefault(s => s.Id == request.SessionId);
        if (session == null) throw new VenueApplicationException(VenueApplicationErrorCode.SessionDoesNotExist);

        var seatingDtos = session.SeatingState.StateSeats
            .Select(s => new SeatingStateDto(s.Row, s.Occupied, s.Type, s.SeatNumber)).ToList();
        
        return seatingDtos;
    }
}