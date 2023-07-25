using MediatR;
using VenueService.Application.DTOs;
using VenueService.Application.Persistence;

namespace VenueService.Application.Queries;

public class GetTheaterSessionsQuery: IRequest<List<TheaterSessionDto>>
{
    public Guid VenueId { get; set; }
    public Guid TheaterId { get; set; }
    
    public GetTheaterSessionsQuery(Guid venueId, Guid theaterId)
    {
        VenueId = venueId;
        TheaterId = theaterId;
    }
}

public class GetTheaterSessionsQueryHandler : IRequestHandler<GetTheaterSessionsQuery, List<TheaterSessionDto>>
{
    private readonly IVenueRepository _venueRepository;

    public GetTheaterSessionsQueryHandler(IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository;
    }
    
    async Task<List<TheaterSessionDto>> IRequestHandler<GetTheaterSessionsQuery, List<TheaterSessionDto>>.Handle(GetTheaterSessionsQuery request, CancellationToken cancellationToken)
    {
        var venue = await _venueRepository.GetById(request.VenueId);

        if (venue == null) throw new Exception(); //Not found

        var sessionDtos = venue.Theaters.Where(t => t.Id == request.TheaterId).SelectMany(t => t.Sessions)
            .Select(s => new TheaterSessionDto(s.TimeRange.Start,
                s.TimeRange.End,
                s.Localization,
                s.SeatingState.Capacity,
                s.MovieId,
                s.Pricings))
            .ToList();
        
        return sessionDtos;
    }
}