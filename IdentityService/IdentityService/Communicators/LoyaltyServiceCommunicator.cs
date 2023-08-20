namespace IdentityService.Communicators;

public class LoyaltyServiceCommunicator: CommunicatorBase
{
    public override string ServiceName { get; } = "LoyaltyService";

    public LoyaltyServiceCommunicator(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }
    
    
}