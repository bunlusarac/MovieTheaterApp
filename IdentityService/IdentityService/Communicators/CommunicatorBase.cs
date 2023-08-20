namespace IdentityService.Communicators;

public class CommunicatorBase
{
    private readonly HttpClient _httpClient;
    public virtual string ServiceName { get; } 
    
    public CommunicatorBase(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(ServiceName);
    }

    private async Task<HttpResponseMessage> SendGetRequest(string route)
    {
        return await _httpClient.GetAsync(route);
    }

    private async Task<HttpResponseMessage> SendPostRequest(string route, HttpContent body)
    {
        return await _httpClient.PostAsync(route, body);
    }
    
    private async Task<HttpResponseMessage> SendPutRequest(string route, HttpContent body)
    {
        return await _httpClient.PutAsync(route, body);
    }
    
    private async Task<HttpResponseMessage> SendDeleteRequest(string route)
    {
        return  await _httpClient.DeleteAsync(route);
    }
}