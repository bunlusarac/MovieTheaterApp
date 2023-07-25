using MediatR;
using VenueService.Application.Persistence;
using VenueService.Domain.Entities;
using VenueService.Domain.Utils;
using VenueService.Domain.ValueObjects;

namespace VenueService.Application.Commands;

public class CreateSessionCommand : IRequest
{
    public Guid VenueId;
    public Guid TheaterId;
    public Guid MovieId;
    public TimeRange TimeRange;
    public Localization Localization;
    public List<Pricing> Pricings;

    public CreateSessionCommand(Guid venueId, Guid theaterId, Guid movieId, TimeRange timeRange, Localization localization, List<Pricing> pricings)
    {
        VenueId = venueId;
        TheaterId = theaterId;
        MovieId = movieId;
        TimeRange = timeRange;
        Localization = localization;
        Pricings = pricings;
    }
}

public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand>
{
    private readonly IVenueRepository _venueRepository;

    public CreateSessionCommandHandler(IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository;
    }

    public async Task Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        var venue = await _venueRepository.GetById(request.VenueId);
        if (venue == null) throw new Exception();
        
        var theater = venue.Theaters.First(t => t.Id == request.TheaterId);
        theater.AddSession(request.TimeRange, request.MovieId, request.Localization, request.Pricings);
        await _venueRepository.Update(venue);
    }
}