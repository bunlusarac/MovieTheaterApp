using MediatR;
using VenueService.Application.DTOs;
using VenueService.Application.Persistence;
using VenueService.Domain.Entities;
using VenueService.Domain.Utils;

namespace VenueService.Application.Commands;

public class CreateVenueCommand: IRequest<VenueCreatedDto>
{
    public string Name { get; set; }
    public Location Location { get; set; }

    public CreateVenueCommand(string name, Location location)
    {
        Name = name;
        Location = location;
    }
}

public class CreateVenueCommandHandler : IRequestHandler<CreateVenueCommand, VenueCreatedDto>
{
    private readonly IVenueRepository _venueRepository;

    public CreateVenueCommandHandler(IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository;
    }

    public async Task<VenueCreatedDto> Handle(CreateVenueCommand request, CancellationToken cancellationToken)
    {
        var venue = new Venue(request.Name, request.Location);
        await _venueRepository.Add(venue);

        return new VenueCreatedDto(venue.Id);
    }
}
