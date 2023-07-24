using MediatR;
using VenueService.Application.DTOs;
using VenueService.Application.Persistence;
using VenueService.Domain.Utils;

namespace VenueService.Application.Queries;

public class GetVenuesQuery: IRequest<List<VenueDto>>
{
    
}

public class GetVenuesQueryHandler : IRequestHandler<GetVenuesQuery, IEnumerable<VenueDto>>
{
    private readonly IVenueRepository _venueRepository;

    public GetVenuesQueryHandler(IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository;
    }
    
    public async Task<IEnumerable<VenueDto>> Handle(GetVenuesQuery request, CancellationToken cancellationToken)
    {
        var venues = await _venueRepository.GetAll();

        var venueDtos = venues.Select(v => new VenueDto(
            v.Name,
            v.Location,
            v.Theaters.Select(t => t.Type)
        )).ToList();

        return venueDtos;
    }
}