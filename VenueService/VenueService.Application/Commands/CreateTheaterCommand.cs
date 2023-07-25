using MediatR;
using VenueService.Application.Persistence;
using VenueService.Domain.Entities;
using VenueService.Domain.Utils;

namespace VenueService.Application.Commands;

public class CreateTheaterCommand: IRequest
{
    public Guid VenueId { get; set; }
    public string Name { get; set; }
    public TheaterType Type { get; set; }
    public int Width { get; set; }

    public CreateTheaterCommand(Guid venueId, string name, TheaterType type, int width)
    {
        VenueId = venueId;
        Name = name;
        Type = type;
        Width = width;
    }
}

public class CreateTheaterCommandHandler : IRequestHandler<CreateTheaterCommand>
{
    private readonly IVenueRepository _venueRepository;

    public CreateTheaterCommandHandler(IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository;
    }
    
    public async Task Handle(CreateTheaterCommand request, CancellationToken cancellationToken)
    {
        var venue = await _venueRepository.GetById(request.VenueId);
        if (venue == null) throw new Exception();
        
        venue.AddTheater(request.Name, request.Width, request.Type);
        await _venueRepository.Update(venue);
    }
}
