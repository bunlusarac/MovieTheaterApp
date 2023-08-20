using Newtonsoft.Json;

namespace BookingService.Infrastructure.Utils;

public static class CommunicatorHelper
{
    public static async Task<HttpResponseMessage> RetryWithExponentialBackoff(Func<Task<HttpResponseMessage>> request, TimeSpan retryInterval, int maxRetries = 0, int backoffExponent = 2)
    {
        maxRetries = maxRetries == 0 ? int.MaxValue : maxRetries;
        
        var retries = 0;

        while (true)
        {
            var response = await request();

            if (response.IsSuccessStatusCode || (maxRetries > 0 && retries >= maxRetries))
            {
                return response;
            }

            await Task.Delay(retryInterval);
            retryInterval *= backoffExponent;
            ++retries;
        }
    }
    
    public static async Task<HttpResponseMessage> RetryWithFixedBackoff(Func<Task<HttpResponseMessage>> request, TimeSpan retryInterval, int maxRetries = 0)
    {
        maxRetries = maxRetries == 0 ? int.MaxValue : maxRetries;
        
        var retries = 0;

        while (true)
        {
            var response = await request();

            if (response.IsSuccessStatusCode || (maxRetries > 0 && retries >= maxRetries))
            {
                return response;
            }

            await Task.Delay(retryInterval);
            ++retries;
        }
    }

    public static async Task<T> DeserializeResponseToType<T>(HttpResponseMessage responseMessage)
    {
        var json = await responseMessage.Content.ReadAsStringAsync();
        var type = JsonConvert.DeserializeObject<T>(json);
        return type;
    }
}