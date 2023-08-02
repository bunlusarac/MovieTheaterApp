using System.Text.Json.Serialization;

namespace LoyaltyService.Domain.Utils;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CampaignType
{
    TicketDiscount,
    CounterItemDiscount,
}