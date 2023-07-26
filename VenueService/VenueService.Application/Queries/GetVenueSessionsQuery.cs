using MediatR;
using VenueService.Application.Commands;
using VenueService.Application.DTOs;
using VenueService.Application.Exceptions;
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

public class GetVenueSessionsQueryHandler : IRequestHandler<GetVenueSessionsQuery, List<VenueSessionDto>>
{
    private readonly IVenueRepository _venueRepository;

    public GetVenueSessionsQueryHandler(IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository;
    }
    
    async Task<List<VenueSessionDto>> IRequestHandler<GetVenueSessionsQuery, List<VenueSessionDto>>.Handle(GetVenueSessionsQuery request, CancellationToken cancellationToken)
    {
        var venue = await _venueRepository.GetById(request.VenueId);

        if (venue == null) throw new VenueApplicationException(VenueApplicationErrorCode.VenueDoesNotExist); //Not found
        
        var sessionDtos = venue.Theaters.SelectMany(t => t.Sessions.Select(s => 
            new VenueSessionDto(
                s.Id,
                s.TimeRange.Start,
                s.TimeRange.End,
                s.Localization,
                s.SeatingState.Capacity,
                s.MovieId,
                s.Pricings,
                t.Id,
                t.Name)
        )).ToList();
        
        return sessionDtos;
    }
}