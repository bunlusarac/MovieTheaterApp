using MediatR;
using VenueService.Application.DTOs;
using VenueService.Application.Exceptions;
using VenueService.Application.Persistence;

namespace VenueService.Application.Queries;

public class GetSessionQuery: IRequest<VenueSessionDto>
{
    public Guid VenueId { get; set; }
    public Guid TheaterId { get; set; }
    public Guid SessionId { get; set; }

    public GetSessionQuery(Guid venueId, Guid theaterId, Guid sessionId)
    {
        VenueId = venueId;
        TheaterId = theaterId;
        SessionId = sessionId;
    }
}

public class GetSessionQueryHandler : IRequestHandler<GetSessionQuery, VenueSessionDto>
{
    private readonly IVenueRepository _venueRepository;

    public GetSessionQueryHandler(IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository;
    }

    public async Task<VenueSessionDto> Handle(GetSessionQuery request, CancellationToken cancellationToken)
    {
        var venue = await _venueRepository.GetById(request.VenueId);
        if (venue == null) throw new VenueApplicationException(VenueApplicationErrorCode.VenueDoesNotExist);

        var theater = venue.Theaters.FirstOrDefault(t => t.Id == request.TheaterId);
        if (theater == null) throw new VenueApplicationException(VenueApplicationErrorCode.TheaterDoesNotExist);

        var session = theater.Sessions.FirstOrDefault(s => s.Id == request.SessionId);
        if (session == null) throw new VenueApplicationException(VenueApplicationErrorCode.SessionDoesNotExist);

        return new VenueSessionDto(
            session.Id,
            session.TimeRange.Start,
            session.TimeRange.End,
            session.Localization,
            session.SeatingState.Capacity,
            session.MovieId,
            session.Pricings,
            theater.Id,
            theater.Name);
    }
}