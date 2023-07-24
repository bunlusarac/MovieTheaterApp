using MediatR;
using VenueService.Application.Commands;
using VenueService.Application.DTOs;
using VenueService.Application.Persistence;
using VenueService.Domain.Entities;

namespace VenueService.Application.Queries;

public class GetVenueSessionsQuery: IRequest<List<VenueSessionDto>>
{
    public Guid VenueId { get; set; }

    public GetVenueSessionsQuery(Guid venueId)
    {
        VenueId = venueId;
    }
}

public class GetVenueSessionsQueryHandler : IRequestHandler<GetVenueSessionsQuery, IEnumerable<VenueSessionDto>>
{
    private readonly IVenueRepository _venueRepository;

    public GetVenueSessionsQueryHandler(IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository;
    }
    
    async Task<IEnumerable<VenueSessionDto>> IRequestHandler<GetVenueSessionsQuery, IEnumerable<VenueSessionDto>>.Handle(GetVenueSessionsQuery request, CancellationToken cancellationToken)
    {
        var venue = await _venueRepository.GetById(request.VenueId);

        var sessionDtos = venue.Theaters.SelectMany(t => t.Sessions.Select(s => 
            new VenueSessionDto(
                s.TimeRange.Start,
                s.TimeRange.End,
                s.Localization,
                s.SeatingState.Capacity,
                s.MovieId,
                s.Pricings)
        ));
        
        return sessionDtos;
    }
}