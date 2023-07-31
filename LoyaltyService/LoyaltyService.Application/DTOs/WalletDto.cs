using LoyaltyService.Domain.ValueObjects;

namespace LoyaltyService.Application.DTOs;

public class WalletDto
{
    public Guid WalletId { get; set; }
    public Guid LoyaltyCustomerId { get; set; }
    public decimal PointsBalance { get; set; }
}