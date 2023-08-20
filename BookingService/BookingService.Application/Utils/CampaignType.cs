using System.Text.Json.Serialization;

namespace BookingService.Application.Utils;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CampaignType
{
    TicketDiscount,
    CounterItemDiscount,
}