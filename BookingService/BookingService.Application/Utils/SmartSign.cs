using System.Text.Json.Serialization;

namespace BookingService.Domain.Utils;

/// <summary>
/// An enum specifying smart signs defined by RTUK that are assigned for a movie.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SmartSign
{
    GeneralAudience,
    SevenOver,
    ThirteenOver,
    EighteenOver,
    NegativeExample,
    ViolenceFear,
    Sexuality,
}