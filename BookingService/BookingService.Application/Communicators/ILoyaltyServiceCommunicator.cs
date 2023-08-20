using BookingService.Application.DTOs;
using BookingService.Infrastructure.DTOs;

namespace BookingService.Application.Communicators;

public interface ILoyaltyServiceCommunicator: ICommunicatorBase
{
    public Task<CampaignRedeemedDto> SendRedeemCampaignRequest(Guid campaignId, Guid customerId, string campaignConcurrencyToken);
    public Task SendRefundCampaignRequest(Guid redeemId, Guid customerId);
    public Task<CampaignDto> SendGetCampaignRequest(Guid campaignId);
}