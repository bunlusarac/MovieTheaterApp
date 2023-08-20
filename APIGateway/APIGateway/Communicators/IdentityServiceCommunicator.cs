using System.Net.Http.Headers;
using System.Net.Mime;
using APIGateway.DTOs;
using Newtonsoft.Json;

namespace APIGateway.Communicators;

public class IdentityServiceCommunicator: CommunicatorBase
{
    public override string ServiceName { get; set; } = "IdentityService";

    public IdentityServiceCommunicator(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    public async Task<ShortSessionValidityDto> RequestShortSessionValidation(Guid userId, string shortSessionToken)
    {
        var bodyDto = new ValidateShortSessionDto { ShortSessionToken = shortSessionToken };
        var bodyContent = new StringContent(JsonConvert.SerializeObject(bodyDto));
        
        bodyContent.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);
        
        var response = await SendPostRequest($"short-session/validate/{userId}", bodyContent);
        if (!response.IsSuccessStatusCode) throw new InvalidOperationException(); //TODO

        var dtoJson = await response.Content.ReadAsStringAsync();
        var dto = JsonConvert.DeserializeObject<ShortSessionValidityDto>(dtoJson);
        return dto;
    }
    
}