using MediatR;
using VenueService.Application.DTOs;
using VenueService.Application.Persistence;
using VenueService.Domain.Entities;

namespace VenueService.Application.Queries;

public class GetSessionSeatingStateQuery: IRequest<SeatingStateDto>
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

public class GetSessionSeatingStateQueryHandler : IRequestHandler<GetSessionSeatingStateQuery, SeatingStateDto>
{
    private readonly IVenueRepository _venueRepository;

    public GetSessionSeatingStateQueryHandler(IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository;
    }
    
    public async Task<SeatingStateDto> Handle(GetSessionSeatingStateQuery request, CancellationToken cancellationToken)
    {
        var venue = await _venueRepository.GetById(request.VenueId);
        
        var session = venue.Theaters.First(s => s.Id == request.SessionId).Sessions.First(s => s.Id == request.SessionId);

        var dto = new SeatingStateDto(session.SeatingState.StateSeats);
        return dto;
    }
}