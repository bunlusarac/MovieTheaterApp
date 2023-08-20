using BookingService.Application.DTOs;

namespace BookingService.Application.Communicators;

public interface IMovieServiceCommunicator: ICommunicatorBase
{
    public Task<MovieDto> SendGetMovieRequest(Guid movieId);
}