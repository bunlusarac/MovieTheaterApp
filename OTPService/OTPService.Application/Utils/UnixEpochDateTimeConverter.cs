using Newtonsoft.Json;

namespace OTPService.Application.Utils;

public class UnixEpochDateTimeConverter: JsonConverter<DateTime>
{
    public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
    {
        var epoch = new DateTimeOffset(value).ToUnixTimeSeconds();
        writer.WriteValue(epoch);
    }

    public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        if(reader.Value == null) return DateTime.MinValue;

        var epoch = Convert.ToInt64(reader.Value);
        return DateTimeOffset.FromUnixTimeSeconds(epoch).DateTime;
    }
}