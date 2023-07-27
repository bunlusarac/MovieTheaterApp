using MediatR;
using VenueService.Application.Exceptions;
using VenueService.Application.Persistence;

namespace VenueService.Application.Commands;

public class DeleteSessionCommand: IRequest
{
    public Guid VenueId { get; set; }
    public Guid TheaterId { get; set; }
    public Guid SessionId { get; set; }

    public DeleteSessionCommand(Guid venueId, Guid theaterId, Guid sessionId)
    {
        VenueId = venueId;
        TheaterId = theaterId;
        SessionId = sessionId;
    }
}

public class DeleteSessionCommandHandler : IRequestHandler<DeleteSessionCommand>
{
    private readonly IVenueRepository _venueRepository;

    public DeleteSessionCommandHandler(IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository;
    }
    
    public async Task Handle(DeleteSessionCommand request, CancellationToken cancellationToken)
    {
        var venue = await _venueRepository.GetById(request.VenueId);
        if (venue == null) throw new VenueApplicationException(VenueApplicationErrorCode.VenueDoesNotExist);

        var theater = venue.Theaters.FirstOrDefault(t => t.Id == request.TheaterId);
        if (theater == null) throw new  VenueApplicationException(VenueApplicationErrorCode.TheaterDoesNotExist);
        
        theater.DeleteSession(request.SessionId);
        
        await _venueRepository.Update(venue);
    }
}



