using LoyaltyService.Domain.ValueObjects;

namespace LoyaltyService.Application.DTOs;

public class WalletDto
{
    public Guid LoyaltyCustomerId { get; set; }
    public Guid CustomerId { get; set; }
    public PointsAmount PointsBalance { get; set; }
}