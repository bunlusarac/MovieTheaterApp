using BookingService.Application.Communicators;
using BookingService.Application.DTOs;
using Newtonsoft.Json;

namespace BookingService.Infrastructure.Communicators;

public class MovieServiceCommunicator: CommunicatorBase, IMovieServiceCommunicator
{
    public override string ServiceName { get; } = "MovieService";
    public async Task<MovieDto> SendGetMovieRequest(Guid movieId)
    {
        var response = await SendGetRequest($"movies/{movieId}");
        if (!response.IsSuccessStatusCode) throw new InvalidOperationException(); //TODO

        var dtoJson = await response.Content.ReadAsStringAsync();
        var dto = JsonConvert.DeserializeObject<MovieDto>(dtoJson);
        return dto;
    }

    public MovieServiceCommunicator(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
        
    }
    
    
}