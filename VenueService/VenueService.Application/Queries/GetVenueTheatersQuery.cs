using MediatR;
using VenueService.Application.Commands;
using VenueService.Application.DTOs;
using VenueService.Application.Exceptions;
using VenueService.Application.Persistence;
using VenueService.Domain.Entities;

namespace VenueService.Application.Queries;

public class GetVenueTheatersQuery: IRequest<List<VenueTheaterDto>>
{
    public Guid VenueId { get; set; }

    public GetVenueTheatersQuery(Guid venueId)
    {
        VenueId = venueId;
    }
}

public class GetVenueTheatersQueryHandler : IRequestHandler<GetVenueTheatersQuery, List<VenueTheaterDto>>
{
    private readonly IVenueRepository _venueRepository;

    public GetVenueTheatersQueryHandler(IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository;
    }
    
    async Task<List<VenueTheaterDto>> IRequestHandler<GetVenueTheatersQuery, List<VenueTheaterDto>>.Handle(GetVenueTheatersQuery request, CancellationToken cancellationToken)
    {
        var venue = await _venueRepository.GetById(request.VenueId);

        if (venue == null) throw new VenueApplicationException(VenueApplicationErrorCode.VenueDoesNotExist);//Not found
        
        var theaterDtos = venue.Theaters.Select(t => new VenueTheaterDto(t.Id, t.Name, t.Type)).ToList();
        
        return theaterDtos;
    }
}