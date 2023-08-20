using System.Net.Http.Headers;
using System.Net.Mime;
using BookingService.Application.Communicators;
using BookingService.Application.DTOs;
using BookingService.Infrastructure.DTOs;
using Newtonsoft.Json;

namespace BookingService.Infrastructure.Communicators;

public class LoyaltyServiceCommunicator: CommunicatorBase, ILoyaltyServiceCommunicator
{
    public override string ServiceName { get; } = "LoyaltyService";

    public LoyaltyServiceCommunicator(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
        
    }


    public async Task<CampaignRedeemedDto> SendRedeemCampaignRequest(Guid campaignId, Guid customerId, string campaignConcurrencyToken)
    {
        var bodyDto = new RedeemCampaignDto
        {
            Version = campaignConcurrencyToken
        };

        var bodyContent = new StringContent(JsonConvert.SerializeObject(bodyDto));
        bodyContent.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);
        
        var response = await SendPutRequest($"customer/{customerId}/redeem/{campaignId}", bodyContent);
        if (!response.IsSuccessStatusCode) throw new InvalidOperationException();

        var redeemJson = await response.Content.ReadAsStringAsync();
        var redeemDto = JsonConvert.DeserializeObject<CampaignRedeemedDto>(redeemJson);

        return redeemDto;
    }

    public async Task SendRefundCampaignRequest(Guid redeemId, Guid customerId)
    {
        var response = await SendPutRequest($"customer/{customerId}/refund/{redeemId}", new StringContent(""));
        if (!response.IsSuccessStatusCode) throw new InvalidOperationException();
    }

    public async Task<CampaignDto> SendGetCampaignRequest(Guid campaignId)
    {
        var response = await SendGetRequest($"campaign/{campaignId}/with-versioning");
        if (!response.IsSuccessStatusCode) throw new InvalidOperationException(); //TODO 
        var dtoJson = await response.Content.ReadAsStringAsync();
        var dto = JsonConvert.DeserializeObject<CampaignDto>(dtoJson);
        return dto;
    }
}