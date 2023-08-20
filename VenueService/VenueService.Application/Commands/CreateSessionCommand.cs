using MediatR;
using VenueService.Application.DTOs;
using VenueService.Application.Exceptions;
using VenueService.Application.Persistence;
using VenueService.Domain.Entities;
using VenueService.Domain.Utils;
using VenueService.Domain.ValueObjects;

namespace VenueService.Application.Commands;

public class CreateSessionCommand : IRequest<SessionCreatedDto>
{
    public Guid VenueId;
    public Guid TheaterId;
    public Guid MovieId;
    public TimeRange TimeRange;
    public Localization Localization;
    public List<PricingDto> Pricings;

    public CreateSessionCommand(Guid venueId, Guid theaterId, Guid movieId, TimeRange timeRange, Localization localization, List<PricingDto> pricings)
    {
        VenueId = venueId;
        TheaterId = theaterId;
        MovieId = movieId;
        TimeRange = timeRange;
        Localization = localization;
        Pricings = pricings;
    }
}

public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, SessionCreatedDto>
{
    private readonly IVenueRepository _venueRepository;

    public CreateSessionCommandHandler(IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository;
    }

    public async Task<SessionCreatedDto> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        var venue = await _venueRepository.GetById(request.VenueId);
        if (venue == null) throw new VenueApplicationException(VenueApplicationErrorCode.VenueDoesNotExist);
        
        var theater = venue.Theaters.FirstOrDefault(t => t.Id == request.TheaterId);
        if (theater == null) throw new  VenueApplicationException(VenueApplicationErrorCode.TheaterDoesNotExist);

        var pricings = request.Pricings.Select(dto => new Pricing(dto.Type, new Price(dto.Amount, dto.Currency)))
            .ToList();
        
        var session = theater.AddSession(request.TimeRange, request.MovieId, request.Localization, pricings);

        await _venueRepository.Update(venue);

        return new SessionCreatedDto(Guid.NewGuid());
        //return new SessionCreatedDto(session.Id);
    }
}