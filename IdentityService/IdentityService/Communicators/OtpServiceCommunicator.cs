namespace IdentityService.Communicators;

public class OtpServiceCommunicator: CommunicatorBase
{
    public override string ServiceName { get; } = "OtpService";

    public OtpServiceCommunicator(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }
}