using VenueService.Domain.Common;
using VenueService.Domain.Utils;
using VenueService.Domain.ValueObjects;

namespace VenueService.Domain.Entities;

public class Pricing: EntityBase
{
    public TicketType Type { get; set; }
    public virtual Price Price { get; set; }
    public Guid SessionId { get; set; }
    
    public Pricing(TicketType type, Price price)
    {
        Type = type;
        Price = price;
    }

    public Pricing()
    {
    }
}