using MediatR;
using VenueService.Application.DTOs;
using VenueService.Application.Persistence;

namespace VenueService.Application.Queries;

public class GetSessionSeatingStateQuery: IRequest<SeatingStateDto>
{
    public Guid SessionId { get; set; }

    public GetSessionSeatingStateQuery(Guid sessionId)
    {
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
        var queryable = await _venueRepository.GetAllQueryable();
            
        var query = 
            queryable
                .SelectMany(v => v.Theaters, (v, t) => new { Venue = v, Theater = t })
                .SelectMany(x => x.Theater.Sessions, (x, s) => new { x.Venue, Session = s })
                .Where(x => x.Session.Id == request.SessionId)
                .Select(x => x.Venue);
        
        var venue = query.ToList().First();
        if (venue == null) throw new Exception();
        
        var session = venue.Theaters.First(s => s.Id == request.SessionId).Sessions.First(s => s.Id == request.SessionId);

        var dto = new SeatingStateDto
        {
            State = session.SeatingState.State
        };

        return dto;
    }
}