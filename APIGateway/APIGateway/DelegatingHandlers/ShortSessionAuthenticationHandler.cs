using System.Net;
using APIGateway.Communicators;

namespace APIGateway.DelegatingHandlers;

public class ShortSessionAuthenticationHandler: DelegatingHandler
{
    private readonly ILogger _logger;
    private readonly IdentityServiceCommunicator _identityServiceCommunicator;

    public ShortSessionAuthenticationHandler(ILogger<ShortSessionAuthenticationHandler> logger, IdentityServiceCommunicator identityServiceCommunicator)
    {
        _logger = logger;
        _identityServiceCommunicator = identityServiceCommunicator;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        _logger.Log(LogLevel.Information, "Auth reached!");

        string? shortToken;
        string? sub; 
        
        try
        {
            shortToken = request.Headers.GetValues("Short-Token").FirstOrDefault();
            sub = request.Headers.GetValues("Customer-Id").FirstOrDefault();
        }
        catch(Exception e)
        {
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }
        
        //Send short token and current user to ValidateShortSessionTokenHandler
        var validity = await _identityServiceCommunicator.RequestShortSessionValidation(Guid.Parse(sub), shortToken);

        if (!validity.IsValid) return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        
        return await base.SendAsync(request, cancellationToken);
    }
    
    
}