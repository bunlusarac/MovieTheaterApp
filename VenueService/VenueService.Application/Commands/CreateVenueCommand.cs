using MediatR;
using VenueService.Application.Persistence;
using VenueService.Domain.Entities;
using VenueService.Domain.Utils;

namespace VenueService.Application.Commands;

public class CreateVenueCommand: IRequest
{
    public string Name { get; set; }
    public Location Location { get; set; }

    public CreateVenueCommand(string name, Location location)
    {
        Name = name;
        Location = location;
    }
}

public class CreateVenueCommandHandler : IRequestHandler<CreateVenueCommand>
{
    private readonly IVenueRepository _venueRepository;

    public CreateVenueCommandHandler(IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository;
    }
    
    public async Task Handle(CreateVenueCommand request, CancellationToken cancellationToken)
    {
        var venue = new Venue(request.Name, request.Location);
        if (venue == null) throw new Exception();

        await _venueRepository.Add(venue);
    }
}
