using System.Text.Json.Serialization;

namespace BookingService.Domain.Utils;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TicketType
{
    Standard,
    Student,
    SweetboxStandard,
    SweetboxStudent
}