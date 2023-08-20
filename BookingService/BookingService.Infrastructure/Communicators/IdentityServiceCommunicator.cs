using BookingService.Application.Communicators;
using BookingService.Application.DTOs;
using Newtonsoft.Json;

namespace BookingService.Infrastructure.Communicators;

public class IdentityServiceCommunicator: CommunicatorBase, IIdentityServiceCommunicator
{
    public override string ServiceName { get; } = "IdentityService";
    
    public async Task<UserInfoDto> SendGetUserInfoRequest(string bearer)
    {
        AddBearerHeader(bearer);
        var response = await SendGetRequest("connect/userinfo");
        RemoveBearerHeader();
        
        if (!response.IsSuccessStatusCode) throw new InvalidOperationException();

        var json = await response.Content.ReadAsStringAsync();
        var dto = JsonConvert.DeserializeObject<UserInfoDto>(json);

        return dto;
    }

    public IdentityServiceCommunicator(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
        
    }
    
    
}