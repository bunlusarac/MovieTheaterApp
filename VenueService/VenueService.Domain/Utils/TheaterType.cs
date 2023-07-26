using System.Text.Json.Serialization;

namespace VenueService.Domain.Utils;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TheaterType
{
    Standard2D,
    IMAX,
    Starium,
    GoldClass,
    FourDX,
    MPX,
    Skybox,
    SkyAudiotorium,
    TempurCinema,
    Cinemini,
    PremiumCinema,
    ScreenX,
    DBox
}