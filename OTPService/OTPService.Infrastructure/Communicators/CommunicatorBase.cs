using OTPService.Application.Communicators;

namespace OTPService.Infrastructure.Communicators;

public class CommunicatorBase: IHttpCommunicator
{
    private readonly HttpClient _httpClient;
    public virtual string ServiceName { get; set; }

    public CommunicatorBase(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(ServiceName);
    }

    protected async Task<HttpResponseMessage> SendGetRequest(string route)
    {
        return await _httpClient.GetAsync(route);
    }
    
    protected async Task<HttpResponseMessage> SendPostRequest(string route, HttpContent body)
    {
        return await _httpClient.PostAsync(route, body);
    }
    
    protected async Task<HttpResponseMessage> SendPutRequest(string route, HttpContent body)
    {
        return await _httpClient.PutAsync(route, body);
    }
    
    protected async Task<HttpResponseMessage> SendDeleteRequest(string route)
    {
        return  await _httpClient.DeleteAsync(route);
    }
    
    public void AddBearerHeader(string bearer)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", bearer);
    }
    
    public void RemoveBearerHeader()
    {
        _httpClient.DefaultRequestHeaders.Remove("Authorization");
    }
}