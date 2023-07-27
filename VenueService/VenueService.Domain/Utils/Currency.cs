using System.Text.Json.Serialization;

namespace VenueService.Domain.Utils;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Currency
{
    TRY,
    EUR,
    USD,
}