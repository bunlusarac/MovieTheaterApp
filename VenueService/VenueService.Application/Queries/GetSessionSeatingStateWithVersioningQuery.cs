using MediatR;
using VenueService.Application.DTOs;
using VenueService.Application.Exceptions;
using VenueService.Application.Persistence;
using VenueService.Domain.Entities;
using VenueService.Domain.Utils;

namespace VenueService.Application.Queries;

public class GetSessionSeatingStateWithVersioningQuery: IRequest<List<SeatingStateWithVersioningDto>>
{
    public Guid VenueId { get; set; }
    public Guid TheaterId { get; set; }
    public Guid SessionId { get; set; }

    public GetSessionSeatingStateWithVersioningQuery(Guid venueId, Guid theaterId, Guid sessionId)
    {
        VenueId = venueId;
        TheaterId = theaterId;
        SessionId = sessionId;
    }
}

public class GetSessionSeatingStateWithVersioningQueryHandler : IRequestHandler<GetSessionSeatingStateWithVersioningQuery, List<SeatingStateWithVersioningDto>>
{
    private readonly IVenueRepository _venueRepository;

    public GetSessionSeatingStateWithVersioningQueryHandler(IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository;
    }
    
    public async Task<List<SeatingStateWithVersioningDto>> Handle(GetSessionSeatingStateWithVersioningQuery request, CancellationToken cancellationToken)
    {
        var venue = await _venueRepository.GetById(request.VenueId);
        if (venue == null) throw new VenueApplicationException(VenueApplicationErrorCode.VenueDoesNotExist);

        var theater = venue.Theaters.FirstOrDefault(s => s.Id == request.TheaterId);
        if (theater == null) throw new VenueApplicationException(VenueApplicationErrorCode.TheaterDoesNotExist);
        
        var session = theater.Sessions.FirstOrDefault(s => s.Id == request.SessionId);
        if (session == null) throw new VenueApplicationException(VenueApplicationErrorCode.SessionDoesNotExist);

        var seatingStateDtos = session.SeatingState.StateSeats
            .Select(s => new SeatingStateWithVersioningDto(s.Row,
                s.Occupied,
                s.Type,
                s.SeatNumber,
                ConcurrencyTokenHelper.GenerateConcurrencyToken(s.Version,
                    s.ConcurrencySecret)))
            .ToList();
        
        return seatingStateDtos;
    }
}