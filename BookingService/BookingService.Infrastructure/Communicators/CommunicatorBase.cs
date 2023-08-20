using System.Net.Http.Headers;

namespace BookingService.Infrastructure.Communicators;

public class CommunicatorBase
{
    private readonly HttpClient _httpClient;
    public virtual string ServiceName { get; } 
    
    public CommunicatorBase(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(ServiceName);
    }

    public async Task<HttpResponseMessage> SendGetRequest(string route)
    {
        return await _httpClient.GetAsync(route);
    }
    
    public void AddBearerHeader(string bearer)
    {
        //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);
        _httpClient.DefaultRequestHeaders.Add("Authorization", bearer);
    }
    
    public void RemoveBearerHeader()
    {
        //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);
        _httpClient.DefaultRequestHeaders.Remove("Authorization");
    }
    
    public async Task<HttpResponseMessage> SendPostRequest(string route, HttpContent body)
    {
        return await _httpClient.PostAsync(route, body);
    }
    
    public async Task<HttpResponseMessage> SendPutRequest(string route, HttpContent body)
    {
        return await _httpClient.PutAsync(route, body);
    }
    
    public async Task<HttpResponseMessage> SendDeleteRequest(string route)
    {
        return  await _httpClient.DeleteAsync(route);
    }
}