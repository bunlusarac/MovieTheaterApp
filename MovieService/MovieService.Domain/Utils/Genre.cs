using System.Text.Json.Serialization;

namespace MovieService.Domain.Utils;

/// <summary>
/// An enum for specifying genre of a movie.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Genre
{
    Action,
    Adventure,
    Comedy,
    Drama,
    Fantasy,
    Horror,
    Mystery,
    Romance,
    SciFi,
    Thriller,
    Western,
    Animation,
    Documentary,
    Musical,
    Crime,
    Family,
    History,
    War,
    Sport,
    Biography
}