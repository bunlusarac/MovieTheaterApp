using Newtonsoft.Json;
using OTPService.Application.Communicators;
using OTPService.Application.DTOs;

namespace OTPService.Infrastructure.Communicators;

public class IdentityServiceCommunicator: CommunicatorBase, IIdentityServiceCommunicator
{
    public override string ServiceName { get; set; } = "IdentityService";

    public IdentityServiceCommunicator(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    public async Task<ShortSessionCreatedDto> RequestShortSessionCreation(Guid userId)
    {
        //var body = new StringContent(JsonConvert.SerializeObject(userId));
        var response = await SendPostRequest($"/short-session/create/{userId}", new StringContent(""));
        if (!response.IsSuccessStatusCode) throw new InvalidOperationException(); //TODO

        var dtoJson = await response.Content.ReadAsStringAsync();
        var dto = JsonConvert.DeserializeObject<ShortSessionCreatedDto>(dtoJson);
        return dto;
    }
}