using System.ComponentModel;
using Newtonsoft.Json.Converters;

namespace VenueService.API.Utils;

public class VenueDateTimeConverter: IsoDateTimeConverter
{
    public VenueDateTimeConverter()
    {
        base.DateTimeFormat = "hh:mm dd-MM-yyyy";
    }
}