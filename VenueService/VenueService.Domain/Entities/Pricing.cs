using VenueService.Domain.Common;
using VenueService.Domain.Utils;
using VenueService.Domain.ValueObjects;

namespace VenueService.Domain.Entities;

public class Pricing: EntityBase
{
    public Dictionary<TicketType, Price> Prices;

    public Pricing(Dictionary<TicketType, Price> prices)
    {
        Prices = prices;
    }
}