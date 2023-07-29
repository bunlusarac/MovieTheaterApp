using System.Text.Json.Serialization;

namespace MovieService.Domain.Utils;

/// <summary>
/// An enum for specifying a theater format.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Format
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