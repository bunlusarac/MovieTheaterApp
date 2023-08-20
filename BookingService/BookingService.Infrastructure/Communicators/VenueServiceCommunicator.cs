using System.Net.Http.Headers;
using System.Net.Mime;
using BookingService.Application.Communicators;
using BookingService.Application.DTOs;
using BookingService.Infrastructure.DTOs;
using Newtonsoft.Json;

namespace BookingService.Infrastructure.Communicators;

public class VenueServiceCommunicator: CommunicatorBase, IVenueServiceCommunicator
{
    public override string ServiceName { get; } = "VenueService";

    public VenueServiceCommunicator(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
        
    }


    public async Task SendReserveSeatRequest(Guid venueId, Guid theaterId, Guid sessionId, int seatNumber, char seatRow,
        string seatConcurrencyToken)
    {
        var bodyDto = new ReserveSeatDto
        {
            SeatNumber = seatNumber,
            SeatRow = seatRow,
            Version = seatConcurrencyToken
        };

        var bodyContent = new StringContent(JsonConvert.SerializeObject(bodyDto));
        bodyContent.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);
        
        var response = await SendPostRequest($"venue/{venueId}/theater/{theaterId}/session/{sessionId}/reserve", bodyContent);
        if (!response.IsSuccessStatusCode) throw new InvalidOperationException();
    }

    public async Task SendRefundSeatRequest(Guid venueId, Guid theaterId, Guid sessionId, int seatNumber, char seatRow/*, string seatConcurrencyToken*/)
    {
        var bodyDto = new RefundSeatDto
        {
            SeatNumber = seatNumber,
            SeatRow = seatRow,
            /*Version = seatConcurrencyToken*/
        };

        var bodyContent = new StringContent(JsonConvert.SerializeObject(bodyDto));
        bodyContent.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);
        
        var response = await SendPostRequest($"venue/{venueId}/theater/{theaterId}/session/{sessionId}/release", bodyContent);
        if (!response.IsSuccessStatusCode) throw new InvalidOperationException();
    }

    public async Task<SessionDto> SendGetSessionRequest(Guid venueId, Guid theaterId, Guid sessionId)
    {
        var response = await SendGetRequest($"venue/{venueId}/theater/{theaterId}/session/{sessionId}");
        if (!response.IsSuccessStatusCode) throw new InvalidOperationException();

        var json = await response.Content.ReadAsStringAsync();
        var dto = JsonConvert.DeserializeObject<SessionDto>(json);

        return dto;
    }
}