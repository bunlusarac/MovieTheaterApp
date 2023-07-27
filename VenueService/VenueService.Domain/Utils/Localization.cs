using System.Text.Json.Serialization;

namespace VenueService.Domain.Utils;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Localization
{
    Subtitle,
    Dubbing
}